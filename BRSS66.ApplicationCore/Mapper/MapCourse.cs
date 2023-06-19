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
            Tittle = model.Title,
            Credit = model.Credits
        };
        return courseResponse;
    }
}