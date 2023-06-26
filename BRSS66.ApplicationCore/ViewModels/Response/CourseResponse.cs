using BRSS66.ApplicationCore.Entities;

namespace BRSS66.ApplicationCore.ViewModels.Response;

public class CourseResponse
{
    public int Id { get; set; }
    public IEnumerable<StudentResponse>? Students { get; set; }
    public string? Title { get; set; }
    public double? Credit { get; set; }
}