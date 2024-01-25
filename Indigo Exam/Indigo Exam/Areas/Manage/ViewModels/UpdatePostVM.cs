namespace Indigo_Exam.Areas.Manage.ViewModels
{
    public class UpdatePostVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
    }
}
