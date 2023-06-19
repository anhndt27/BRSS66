using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Models;
using BRSS66.Database.Context;

namespace BRSS66.Infrastructure.Repositories;

public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Course>> GetData(JqueryDatatableParam param)
    {
        throw new NotImplementedException();
    }
}