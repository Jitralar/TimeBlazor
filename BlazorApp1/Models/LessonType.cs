namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class LessonType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
