namespace dxxt.Models
{
    public class StudentSubSco
    {
        public DateTime Intake { get; set; }
        public string? Email { get; set; }
        public int ScoreID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public string? StudentName { get; set; }
        public string? SubjectName { get; set; }
        public decimal? Marks { get; set; }
        public bool IsDelete { get; set; }
    }
}
