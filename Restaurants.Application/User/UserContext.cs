using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
       var user = httpContextAccessor.HttpContext?.User;
       
       if (user == null)
           throw new InvalidOperationException("User context is null");

       if (user.Identity == null || !user.Identity.IsAuthenticated)
           return null;
       
       var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
       var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
       var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
       var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
       var birthDateString = user.FindFirst(c => c.Type == "BirthDate")?.Value;
       var birthDate = birthDateString == null 
           ? (DateOnly?) null 
           : DateOnly.ParseExact(birthDateString, "yyyy-MM-dd");
       
       return new CurrentUser(userId, userEmail, userRoles, nationality, birthDate);
    }
}