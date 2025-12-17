namespace BlazorApp1.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string DepartmentCode { get; set; } = string.Empty;

    public string SubjectCode { get; set; } = string.Empty;

    public int Credits { get; set; }
}
