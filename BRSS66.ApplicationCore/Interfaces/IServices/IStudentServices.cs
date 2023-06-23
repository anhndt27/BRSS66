using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface IStudentServices
{
    Task<PagedResponse<StudentResponse>> GetDataAsync(DataTablesRequest param);
    Task<bool> CreateAsync(StudentRequest model);
    Task<StudentResponse> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, StudentRequest model);
    Task<bool> DeleteAsync(int id);
}