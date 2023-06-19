using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.Entities;

public class Student : BaseEntity
{
    [Required]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 30 characters.")]
    [DisplayName("Full Name")]
    public string? Name { get; set; }
    
    [Required]
    [DisplayName("Code")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 10 characters.")]
    public string? Code { get; set; }
    
    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}