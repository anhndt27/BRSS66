using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Mapper;

public static class MapCourse
{
    public static Course MapToCoures(this CourseRequest model)
    {
        Course course = new Course()
        {
            Title = model.Title,
            Credits = model.Credit,
        };
        return course;
    }

    public static CourseResponse MapToCourseResponse(this Course model)
    {
        CourseResponse courseResponse = new CourseResponse()
        {
            Id = model.Id,
            Title = model.Title,
            Credit = model.Credits,
            /*Students = model.Enrollments!.Select(e => new StudentResponse
            {
                Id = e.Student!.Id,
                Name = e.Student.Name,
                Code = e.Student.Code
            
            })*/
        };
        return courseResponse;
    }

    public static IQueryable<CourseResponse> MapListCourseDto(this IQueryable<Course> model)
    {
        return model.Select(c => new CourseResponse
        {
            Id = c.Id,
            Title = c.Title,
            Credit = c.Credits,
            Students = c.Enrollments!.Select(e => new StudentResponse
            {
                Id = e.Student!.Id,
                Name = e.Student.Name,
                Code = e.Student.Code,
            })
        });
    }
}