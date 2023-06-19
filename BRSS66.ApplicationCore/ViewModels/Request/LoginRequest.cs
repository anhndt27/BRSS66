using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.ViewModels;

public class LoginRequest
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool Checkbox { get; set; }
}

