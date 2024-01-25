using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace Indigo_Exam.Areas.Manage.ViewModels
{
    public class LogInVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
