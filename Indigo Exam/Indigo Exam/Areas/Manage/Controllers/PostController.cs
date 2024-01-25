using Indigo_Exam.Areas.Manage.ViewModels;
using Indigo_Exam.DAL;
using Indigo_Exam.Models;
using Indigo_Exam.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indigo_Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page=1,int take=4)
        {
            int count = await _context.Posts.CountAsync();
            List<Post> posts = await _context.Posts.Skip((page-1)*take).Take(take).ToListAsync();
            PaginationVM<Post> paginationVM = new PaginationVM<Post>
            {
                Currentpage = page,
                TotalPage = Math.Ceiling((double)count / take),
                Items = posts
            };
            return View(paginationVM);
        }
        public IActionResult Create()
        {
        return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostVM vM)
        {
            if (!ModelState.IsValid) return View();
            if (!vM.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo type is incorrect");
                return View();
            }
            if (!vM.Photo.CheckSize(5))
            {
                ModelState.AddModelError("Photo", "Photo size is bigger than 5 mb");
                return View();
            }
            string filename = await vM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
            Post post = new Post
            {
                Title = vM.Title,
                Description = vM.Description,
                Image = filename
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id )
        {
            if (id <= 0) return BadRequest();
            Post post= await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (post == null) return NotFound();
            UpdatePostVM vm = new UpdatePostVM
            {
                Title = post.Title,
                Description = post.Description,
                Image = post.Image,

            };
            return View(vm);
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdatePostVM vM)
        {
            if (id <= 0) return BadRequest();
            Post existed = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (existed == null) return NotFound();
            if (vM.Photo is not null)
            {
                if (!vM.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "Photo type is incorrect");
                    return View();
                }
                if (!vM.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "Photo size is bigger than 5 mb");
                    return View();
                }
                string filename = await vM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images");
                existed.Image = filename;
            }
            existed.Title = vM.Title;
            existed.Description = vM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Post existed = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "images");
             _context.Posts.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
