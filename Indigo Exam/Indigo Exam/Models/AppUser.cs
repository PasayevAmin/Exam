using Microsoft.AspNetCore.Identity;

namespace Indigo_Exam.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
