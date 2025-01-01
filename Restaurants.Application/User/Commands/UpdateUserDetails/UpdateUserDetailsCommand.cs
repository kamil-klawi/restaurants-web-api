using MediatR;

namespace Restaurants.Application.User.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly? BirthDate { get; set; }
    public string? Nationality { get; set; }
}