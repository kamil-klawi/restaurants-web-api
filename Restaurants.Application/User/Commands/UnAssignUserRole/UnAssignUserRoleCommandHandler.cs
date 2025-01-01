using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(
    ILogger<UnAssignUserRoleCommandHandler> logger, 
    UserManager<Domain.Entities.User> userManager, 
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassigning user role to user");
        var user = await userManager.FindByEmailAsync(request.UserEmail) 
                   ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);
        
        var role = await roleManager.FindByNameAsync(request.RoleName)
                   ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}