namespace BlazorApp1.Models;

public class Classroom
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public int Floor { get; set; }

    public int Capacity { get; set; }

    public string Purpose { get; set; } = string.Empty;
}
