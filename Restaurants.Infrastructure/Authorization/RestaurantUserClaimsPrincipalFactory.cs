using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Constants;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(
    UserManager<User> userManager, 
    RoleManager<IdentityRole> roleManager, 
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        if (user.Nationality != null)
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        
        if (user.BirthDate != null)
            id.AddClaim(new Claim(AppClaimTypes.BirthDate, user.BirthDate.Value.ToString("yyyy-MM-dd")));
        
        return new ClaimsPrincipal(id);
    }
}