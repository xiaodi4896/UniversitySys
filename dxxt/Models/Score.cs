using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dxxt.Models
{
    public class Score
    {
        [Key]
        public int ScoreID { get; set; }
       
        [Required]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student? Student { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public Subject? Subject { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Marks { get; set; }
        [Required]
        
        public bool IsDelete { get; set; }
    }
}
