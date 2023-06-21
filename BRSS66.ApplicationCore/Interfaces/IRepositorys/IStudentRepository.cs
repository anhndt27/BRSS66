using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Models;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Interfaces.IRepositorys;

public interface IStudentRepository : IRepositoryBase<Student>
{
    Task<(List<StudentResponse>, int)> GetPaging(JqueryDatatableParam param);
}