using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BRSS66.ApplicationCore.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int IdentityId { get; set; }
    public bool IsActive { get; set; } = true;
    public AppUser? Identity { get; set; }
}