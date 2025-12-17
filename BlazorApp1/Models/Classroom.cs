namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Classroom
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    public int Floor { get; set; }

    public int Capacity { get; set; }

    public string Purpose { get; set; } = string.Empty;
}
