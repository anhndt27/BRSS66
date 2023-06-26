using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IRepositorys;

public interface ICourseRepository : IRepositoryBase<Course>
{
    Task<PagedResponse<CourseResponse>> GetPaging(DataTablesRequest param);
}