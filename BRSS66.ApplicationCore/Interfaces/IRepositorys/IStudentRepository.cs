using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Models;

namespace BRSS66.ApplicationCore.Interfaces.IRepositorys;

public interface IStudentRepository : IRepositoryBase<Student>
{
    Task<List<Student>> GetPaging(JqueryDatatableParam param);
}