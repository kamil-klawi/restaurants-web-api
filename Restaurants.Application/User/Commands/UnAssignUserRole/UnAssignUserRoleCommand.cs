using MediatR;

namespace Restaurants.Application.User.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}