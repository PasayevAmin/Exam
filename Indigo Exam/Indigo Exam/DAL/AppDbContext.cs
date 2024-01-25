using Indigo_Exam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indigo_Exam.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }


    }
}
