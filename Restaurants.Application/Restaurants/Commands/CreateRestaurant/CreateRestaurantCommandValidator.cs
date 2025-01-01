using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100).NotEmpty().WithMessage("Please specify a name");
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Please specify a description");
        RuleFor(dto => dto.Category).NotEmpty().WithMessage("Please specify a category");
        RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please specify a email");
        RuleFor(dto => dto.ZipCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please specify a zip code");
    }
}