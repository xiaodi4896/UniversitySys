using System.ComponentModel.DataAnnotations.Schema;

namespace dxxt.Models
{
    public class ScoreStuSub
    {
        public int ScoreID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public string? StudentName { get; set; }
        public string? SubjectName { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Marks { get; set; }
        public bool IsDelete { get; set; }
    }
}