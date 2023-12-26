using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dxxt.Models
{
    public class Lecturer
    {
        internal int TotalCount;

        [Key]
        public int LecturerID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string? LecturerName { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
