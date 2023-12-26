namespace dxxt.Models
{
    public class StudentDetailsViewModel
    {
        public int? StudentID { get; set; }
        public string? StudentName { get; set; }
        public DateTime Intake { get; set; }
        public string? Email { get; set; }
        public bool CanEnrollMore { get; set; }
        public IEnumerable<SubjectScoreViewModel>? SubjectsAndScores { get; set; }
    }

    public class SubjectScoreViewModel
    {
        public string? SubjectName { get; set; }
        public decimal? Marks { get; set; }
    }
}
