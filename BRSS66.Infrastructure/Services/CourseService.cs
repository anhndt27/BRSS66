using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.ApplicationCore.Interfaces.IServices;
using BRSS66.ApplicationCore.Mapper;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.Infrastructure.Services;

public class CourseService : ICourseServices
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<Course>> Get()
    {
        return await _courseRepository.Get();
    }

    public async Task<bool> CreateAsync(CourseRequest model)
    {
        try
        {
            if (model == null)
            {
                throw new Exception("Course is required");
            }

            Course course = model.MapToCoures();
            await _courseRepository.Add(course);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CourseResponse> GetByIdAsync(int id)
    {
        try
        {
            var resCourse = await _courseRepository.Get(id);
            if (resCourse == null)
            {
                throw new Exception($"Course id:{id} not found");
            }

            return resCourse.MapToCourseResponse();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, CourseRequest model)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var resCourse = await _courseRepository.Get(id);
            if (resCourse == null)
            {
                throw new Exception($"Course id:{id} not found");
            }

            await _courseRepository.Delete(resCourse);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}