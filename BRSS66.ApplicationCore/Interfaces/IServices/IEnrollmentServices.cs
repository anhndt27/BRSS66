using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface IEnrollmentServices
{
    Task<bool> CreateAsync(EnrollmentRequest model);
}