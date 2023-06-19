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
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            /*Enrollments = model.Enrollments!.Select(enrollment => enrollment.Grade),
            Course = model.Enrollments!.Select(enrollment => enrollment.Course)*/
        };
        return studentResponse;
    }
}