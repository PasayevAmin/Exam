using Indigo_Exam.DAL;
using Microsoft.EntityFrameworkCore;

namespace Indigo_Exam.Service
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingAsync()
        {
            Dictionary<string,string> setting= await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return setting;
        }
    }
}
