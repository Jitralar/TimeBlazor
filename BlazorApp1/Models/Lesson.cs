using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models;

public class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DayOfWeek Day { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string ClassroomCode { get; set; } = string.Empty;

    public string SubjectCode { get; set; } = string.Empty;

    public Guid LessonTypeId { get; set; }

    public Guid LecturerId { get; set; }

    [NotMapped]
    public List<Guid> StudentIds { get; set; } = new();
}
