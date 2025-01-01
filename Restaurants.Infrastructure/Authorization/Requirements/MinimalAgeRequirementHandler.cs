using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimalAgeRequirementHandler(
    ILogger<MinimalAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimalAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimalAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Handling Minimal age requirement");

        if (currentUser?.BirthDate == null)
        {
            logger.LogInformation("No birth date");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.BirthDate.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization success");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        
        return Task.CompletedTask;
    }
}