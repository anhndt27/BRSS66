using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.ViewModels;

public class StudentDto
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Code { get; set; }
}
