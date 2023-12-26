namespace dxxt.Models
{
    public class SubjectLec
    {
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public int LecturerID { get; set; }
        public string? LecturerName { get; set; }
        public bool IsDelete { get; set; }
    }
}
