namespace BRSS66.ApplicationCore.Entities;

public class Enrollment : BaseEntity
{
    public string? Grade { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}