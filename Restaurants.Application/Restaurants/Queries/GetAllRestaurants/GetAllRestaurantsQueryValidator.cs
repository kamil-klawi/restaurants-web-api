using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _allowPageSizes = [ 5, 10, 25, 50 ];
    private readonly string[] _allowedSortByColumnNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
    
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        
        RuleFor(r => r.PageSize)
            .Must(v => _allowPageSizes.Contains(v))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");
        
        RuleFor(r => r.SortBy)
            .Must(v => _allowedSortByColumnNames.Contains(v))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}