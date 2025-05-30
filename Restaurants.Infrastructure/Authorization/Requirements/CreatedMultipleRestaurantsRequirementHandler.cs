using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.User;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreatedMultipleRestaurantsRequirementHandler(
    IRestaurantsRepository restaurantsRepository, 
    IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        var restaurants = await restaurantsRepository.GetAllAsync();
        var userRestaurantsCreated = restaurants.Count(restaurant => restaurant.OwnerId == currentUser!.Id);
        
        if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}