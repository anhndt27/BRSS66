using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.ViewModels.Request;

public class StudentRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Code { get; set; }
}