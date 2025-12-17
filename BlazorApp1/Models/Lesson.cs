using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Models;

public class Lesson
{
    public int Id { get; set; }

    [Required]
    public DayOfWeek Day { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    [Required]
    public string ClassroomCode { get; set; } = string.Empty;

    [Required]
    public string SubjectCode { get; set; } = string.Empty;

    [Required]
    public int LessonTypeId { get; set; }

    [Required]
    public int LecturerId { get; set; }

    [NotMapped]
    public List<int> StudentIds { get; set; } = new();
}
