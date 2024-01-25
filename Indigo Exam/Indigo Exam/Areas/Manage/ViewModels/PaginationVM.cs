namespace Indigo_Exam.Areas.Manage.ViewModels
{
    public class PaginationVM<T> where T : class,new()
    {
        public double TotalPage { get; set; }
        public int Currentpage { get; set; }
        public List<T> Items { get; set; }

    }
}
