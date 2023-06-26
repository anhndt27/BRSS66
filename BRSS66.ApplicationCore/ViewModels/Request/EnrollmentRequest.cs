using BRSS66.ApplicationCore.Enum;

namespace BRSS66.ApplicationCore.ViewModels.Request;

public class EnrollmentRequest
{
    public Grade Grade { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public int[] StudentIdAray { get; set; } = null!;
}