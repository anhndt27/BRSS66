using BRSS66.ApplicationCore.Entities;

namespace BRSS66.ApplicationCore.ViewModels;

public class CourseDto
{
    public int Id { get; set; }
    public IEnumerable<Enrollment>? Students { get; set; }
    public string? Title { get; set; }
    public double? Credit { get; set; }
}