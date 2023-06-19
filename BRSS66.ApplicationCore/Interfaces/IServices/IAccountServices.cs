using BRSS66.ApplicationCore.ViewModels;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface IAccountServices
{
    Task<bool> Login(LoginRequest loginRequest);
}