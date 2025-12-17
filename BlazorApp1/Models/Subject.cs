namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Subject
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string DepartmentCode { get; set; } = string.Empty;

    [Required]
    [StringLength(5, MinimumLength = 3)]
    public string SubjectCode { get; set; } = string.Empty;

    [Range(0, 30)]
    public int Credits { get; set; }
}
