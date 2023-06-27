using BRSS66.ApplicationCore.Entities;

namespace BRSS66.ApplicationCore.ViewModels.Response;

public class StudentResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<CourseResponse>? Course { get; set; }
    
}