using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        RuleFor(command => command.KiloCalories).GreaterThan(0).WithMessage("Kcal must be greater than zero");
    }
}