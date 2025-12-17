namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class LessonType
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;
}
