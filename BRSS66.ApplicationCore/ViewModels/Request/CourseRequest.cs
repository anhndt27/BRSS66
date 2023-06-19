using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.ViewModels.Request;

public class CourseRequest
{
    [Required]
    public string? Title { get; set; }
    [Required] 
    public double Credit { get; set; }
}