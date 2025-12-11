namespace BlazorApp1.Models;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string Affiliation { get; set; } = string.Empty;

    public Guid? RoleId { get; set; }

    public string FullName => string.IsNullOrWhiteSpace(Title)
        ? $"{FirstName} {LastName}"
        : $"{Title} {FirstName} {LastName}";
}
