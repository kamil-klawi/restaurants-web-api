using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(
    ILogger<UpdateUserDetailsCommandHandler> logger, 
    IUserContext userContext,
    IUserStore<Domain.Entities.User> userStore
    ) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating user");
        var user = userContext.GetCurrentUser();
        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        
        if(dbUser == null)
            throw new NotFoundException(nameof(Domain.Entities.User), user!.Id);
        
        dbUser.Nationality = request.Nationality;
        dbUser.BirthDate = request.BirthDate;
        
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}