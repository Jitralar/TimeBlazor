namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Role
{
    public int Id { get; set; }

    [Required]
    public string RoleType { get; set; } = string.Empty;
}
