using System.Data;
using BRSS66.ApplicationCore.Models;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface IStudentServices
{
    Task<(List<StudentResponse>, int)> GetDataAsync(JqueryDatatableParam param);
    Task<bool> CreateAsync(StudentRequest model);
    Task<StudentResponse> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, StudentRequest model);
    Task<bool> DeleteAsync(int id, StudentRequest model);
}