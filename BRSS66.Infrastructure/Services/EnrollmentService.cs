using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.ViewModels.Request;

namespace BRSS66.Infrastructure.Services;

public class EnrollmentService : IEnrollmentServices
{
    public Task<bool> CreateAsync(EnrollmentRequest model)
    {
        throw new NotImplementedException();
    }
}