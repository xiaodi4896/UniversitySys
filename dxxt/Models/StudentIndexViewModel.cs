namespace dxxt.Models
{
    public class StudentIndexViewModel
    {
        public IEnumerable<Student>? Students { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
