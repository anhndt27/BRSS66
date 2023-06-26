using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IServices;

public interface ICourseServices
{
    Task<PagedResponse<CourseResponse>> Get(DataTablesRequest param);
    Task<bool> CreateAsync(CourseRequest model);
    Task<CourseResponse> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, CourseRequest model);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddStudentToCourse(int courseId, int studentId);
}