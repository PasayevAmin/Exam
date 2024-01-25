using Indigo_Exam.Areas.Manage.ViewModels;
using Indigo_Exam.DAL;
using Indigo_Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Indigo_Exam.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page=1,int take=4)
        {
            int count = await _context.Settings.CountAsync();
            List<Setting> settings = await _context.Settings.Skip((page - 1) * take).Take(take).ToListAsync();
            PaginationVM<Setting> paginationVM = new PaginationVM<Setting>
            {
                Currentpage = page,
                TotalPage = Math.Ceiling((double)count / take),
                Items = settings
            };
            return View(paginationVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(post => post.Id == id);
            if (setting == null) return NotFound();
            UpdateSettingVM vm = new UpdateSettingVM
            {
                Key = setting.Key,
                Value = setting.Value,

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingVM vM)
        {
            if (id <= 0) return BadRequest();
            Setting existed = await _context.Settings.FirstOrDefaultAsync(post => post.Id == id);
            if (existed == null) return NotFound();
            existed.Key = vM.Key;
            existed.Value = vM.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
