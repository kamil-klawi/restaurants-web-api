using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Constants;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly string[] Nationality = ["German", "English", "French", "Polish"];
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, policy => policy.RequireClaim(AppClaimTypes.Nationality, Nationality))
            .AddPolicy(PolicyNames.AtLeast20, policy => policy.AddRequirements(new MinimalAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtLeast2Restaurants, policy => policy.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));
        services.AddScoped<IAuthorizationHandler, MinimalAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }
}