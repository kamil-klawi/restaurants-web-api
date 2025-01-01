using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
    ILogger<CreateDishCommandHandler> logger, 
    IMapper mapper, 
    IRestaurantsRepository restaurantsRepository, 
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService
    ) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish {@DishRequest}", request);
        var restaurantId = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurantId == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        if (!restaurantAuthorizationService.Authorize(restaurantId, ResourceOperation.Update))
            throw new ForbiddenException();
        
        var dish = mapper.Map<Dish>(request);
        return await dishesRepository.Create(dish);
    }
}