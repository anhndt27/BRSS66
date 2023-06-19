using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BRSS66.ApplicationCore.DomainService;

public class AccountServices : IAccountServices
{
    private UserManager<User> _userManager;
    private RoleManager<IdentityRole<Guid>> _roleManager;
    private SignInManager<User> _signInManager;

    public AccountServices(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, SignInManager<User> signInManager)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._signInManager = signInManager;

        if (!roleManager.Roles.Any())
        {
            var result = roleManager.CreateAsync(new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", })
                .Result;
            result = roleManager.CreateAsync(new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User" }).Result;

            var user = new User { UserName = "admin@abc.asd", Email = "admin@abc.asd" };
            result = userManager.CreateAsync(user, "Admin@123").Result;
            result = userManager.AddToRoleAsync(user, "Admin").Result;

            user = new User { UserName = "user@abc.asd", Email = "user@abc.asd" };
            result = userManager.CreateAsync(user, "User@123").Result;
            result = userManager.AddToRoleAsync(user, "User").Result;
        }
    }

    public async Task<bool> Login(LoginRequest loginRequest)
    {
        User user = new User();
        //Microsoft.AspNetCore.Identity.SignInResult result;
        if (loginRequest.Password != null
            && loginRequest.Username != null 
            && await _userManager.FindByEmailAsync(loginRequest.Username) != null 
            && (await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false)).Succeeded)
        {
            return true;
        }
        return false;
    }
}
