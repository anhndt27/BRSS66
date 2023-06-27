using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IRepositorys;

public interface IStudentRepository : IRepositoryBase<Student>
{
    Task<PagedResponse<StudentResponse>> GetPaging(DataTablesRequest param);
    Task<List<Student>> Search(string term);
    Task<List<Student>> GetAll();
}