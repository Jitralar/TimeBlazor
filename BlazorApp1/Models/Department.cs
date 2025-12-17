namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}
