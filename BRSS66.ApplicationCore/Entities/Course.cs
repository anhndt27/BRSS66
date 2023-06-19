using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.Entities;

public class Course : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string? Title { get; set; }
    [Required]
    public double Credits { get; set; }
    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}