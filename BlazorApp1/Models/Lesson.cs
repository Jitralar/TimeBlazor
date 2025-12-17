using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models;

public class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public DayOfWeek Day { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    [Required]
    public string ClassroomCode { get; set; } = string.Empty;

    [Required]
    public string SubjectCode { get; set; } = string.Empty;

    [Required]
    public Guid LessonTypeId { get; set; }

    [Required]
    public Guid LecturerId { get; set; }

    [NotMapped]
    public List<Guid> StudentIds { get; set; } = new();
}
