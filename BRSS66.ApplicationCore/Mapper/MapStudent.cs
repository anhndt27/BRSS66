using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.ViewModels.Request;
using BRSS66.ApplicationCore.ViewModels.Response;

namespace BRSS66.ApplicationCore.Mapper;

public static class MapStudent
{
    public static Student MapToStudent(this StudentRequest model)
    {
        Student student = new Student()
        {
            Name = model.Name,
            Code = model.Code
        };
        return student;
    }

    public static StudentResponse MapToStudentResponse(this Student model)
    {
        StudentResponse studentResponse = new StudentResponse()
        {
            Name = model.Name,
            Code = model.Code,
            /*Course = model.Enrollments!.Select(c => new CourseResponse
            {
                Id = c.Course!.Id,
                Title = c.Course.Title,
                Credit = c.Course.Credits
            }),*/
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
        return studentResponse;
    }
    
    public static IQueryable<StudentResponse> MapListStudentDto(this IQueryable<Student> students)
    {
        return students.Select(s => new StudentResponse
        {
            Id = s.Id,
            Name = s.Name,
            Code = s.Code,
            Course = s.Enrollments!.Select(e => new CourseResponse
            {
                Id = e.Course!.Id,
                Title = e.Course.Title,
                Credit = e.Course.Credits
            })
        });
    }
}