using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dxxt.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string? SubjectName { get; set; }
        [Required]
        [ForeignKey("Lecturer")]
        public int LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }
        [Required]
        public bool IsDelete { get; set; }

        public ICollection<Score>? Scores { get; set; }
    }
}
