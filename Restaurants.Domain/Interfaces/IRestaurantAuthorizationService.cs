using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
}