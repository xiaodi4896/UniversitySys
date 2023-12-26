using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace dxxt.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        [Column (TypeName ="nvarchar(20)")]
        public string? StudentName { get; set; }
        [Required]
        [Column(TypeName ="varchar(30)")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        [Required]
        [Column(TypeName ="datetime2")]
        public DateTime Intake { get; set; }
        [Required]
        public bool IsDelete { get; set; }

        public ICollection<Score>? Scores { get; set; }
    }
}
