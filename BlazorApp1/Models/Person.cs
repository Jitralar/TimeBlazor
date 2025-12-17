namespace BlazorApp1.Models;

using System.ComponentModel.DataAnnotations;

public class Person
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string? Title { get; set; }

    [Required]
    public string Affiliation { get; set; } = string.Empty;

    public int? RoleId { get; set; }

    public string FullName => string.IsNullOrWhiteSpace(Title)
        ? $"{FirstName} {LastName}"
        : $"{Title} {FirstName} {LastName}";
}
