using Indigo_Exam.DAL;
using Indigo_Exam.Models;
using Indigo_Exam.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indigo_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _context.Posts.ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Posts = posts,
            };
            return View(homeVM);
        }
    }
}
