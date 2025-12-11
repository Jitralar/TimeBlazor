namespace BlazorApp1.Models;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string RoleType { get; set; } = string.Empty;
}
