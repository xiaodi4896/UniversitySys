namespace dxxt.Models
{
    public class SubjectIndexViewModel
    {
        public IEnumerable<SubjectLec>? Subjects { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
