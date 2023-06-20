using BRSS66.ApplicationCore.Models;
using BRSS66.ApplicationCore.ViewModels.Request;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface IAuthenticationService
{
    Task<bool> Login(LoginDto model);
    Task<bool> Register(RegisterDto model);

    Task<bool> Logout();
}