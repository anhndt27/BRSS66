using System.ComponentModel.DataAnnotations;

namespace BRSS66.ApplicationCore.ViewModels.Request;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name ="Confirm Password")]
    [Compare("Password",ErrorMessage ="Password and confirmation password not match.")]
    public string? ConfirmPassword { get; set; }
}