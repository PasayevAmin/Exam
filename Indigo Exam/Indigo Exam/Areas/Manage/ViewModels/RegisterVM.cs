using System.ComponentModel.DataAnnotations;

namespace Indigo_Exam.Areas.Manage.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        public string Surname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string CinfirmPassword { get; set; }
    }
}
