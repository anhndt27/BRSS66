using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;

namespace BRSS66.ApplicationCore.Interfaces.IRepositorys;

public interface ICourseRepository : IRepositoryBase<Course>
{
    Task<List<Course>> GetData(DataTablesRequest param);
}