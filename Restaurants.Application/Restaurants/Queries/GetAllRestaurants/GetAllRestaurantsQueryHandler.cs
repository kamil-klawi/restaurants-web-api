using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(
    ILogger<GetAllRestaurantsQueryHandler> logger, 
    IMapper mapper, 
    IRestaurantsRepository repository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all restaurants");
        var (restaurants, totalCount) = await repository.GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber, request.SortBy, request.SortDirection);
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        var result = new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}