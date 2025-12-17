namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string RoleType { get; set; } = string.Empty;
}
