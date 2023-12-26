namespace dxxt.Models
{
    public class StudentScoresViewModel
    {
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public List<SubjectScore>? SubjectScores { get; set; }
    }

    public class SubjectScore
    {
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public decimal? Marks { get; set; }
    }
}
