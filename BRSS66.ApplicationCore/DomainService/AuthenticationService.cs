using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;
using Microsoft.AspNetCore.Identity;

namespace BRSS66.ApplicationCore.DomainService;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthenticationService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<bool> Login(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> Register(RegisterDto model)
    {
        var user = new AppUser()
        {
            UserName = model.Email,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            return true;
        }

        return false;
    }

    public async Task<bool> Logout()
    {
        await _signInManager.SignOutAsync();
        return true;
    }
}