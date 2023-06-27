using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.Infrastructure.Services;

public class CourseService : ICourseServices
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;
    public CourseService(ICourseRepository courseRepository,
        IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }
                                   
    public async Task<List<Course>> Get(string search)
    {
        return await _courseRepository.Get(search);
    }

    public async Task<PagedResponse<CourseResponse>> Get(DataTablesRequest param)
    {
        var lstCourse = await _courseRepository.GetPaging(param);
        return lstCourse;
    }

    public async Task<bool> CreateAsync(CourseRequest model)
    {
        if (model == null)
        {
            throw new Exception("Course is required");
        }

        Course course = model.MapToCoures();
        await _courseRepository.Add(course);
        return true;
    }
        
    public async Task<CourseResponse> GetByIdAsync(int id)
    {
        var resCourse = await _courseRepository.Get(id);
        if (resCourse == null)
        {
            throw new Exception($"Course id:{id} not found");
        }

        return resCourse.MapToCourseResponse();
    }

    public async Task<bool> UpdateAsync(int id, CourseRequest model)
    {
        if (model == null)
        {
            throw new Exception("Course is required");
        }

        var resCourse = await _courseRepository.Get(id);
        if (resCourse == null)
        {
            throw new Exception($"Course id:{id} not found");
        }

        await _courseRepository.Update(model.MapToCoures());
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var resCourse = await _courseRepository.Get(id);
        if (resCourse == null)
        {
            throw new Exception($"Course id:{id} not found");
        }

        await _courseRepository.Delete(resCourse);
        return true;
    }

    public async Task<bool> AddStudentToCourse(int courseId, int studentId)
    {
        var entity = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
        };
        await _enrollmentRepository.Add(entity);
        return true;
    }

    public async Task<List<Student>> Search(string term)
    {
        return await _studentRepository.Search(term);
    }

    public async Task<List<Student>> GetAll()
    {
        return await _studentRepository.GetAll();
    }
}