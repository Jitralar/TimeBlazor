namespace BlazorApp1.Models;

public class LessonType
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
}
