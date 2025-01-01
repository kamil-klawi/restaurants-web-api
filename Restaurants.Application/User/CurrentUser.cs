namespace Restaurants.Application.User;

public record CurrentUser(
    string Id, 
    string Email, 
    IEnumerable<string> Roles, 
    string? Nationality, 
    DateOnly? BirthDate)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}