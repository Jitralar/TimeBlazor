namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Department
{
    public int Id { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}
