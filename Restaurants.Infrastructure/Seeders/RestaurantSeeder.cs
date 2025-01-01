using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                dbContext.Restaurants.AddRange(GetRestaurants());
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new (UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper(),
            }, 
            new (UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper(),
            }, 
            new (UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper(),
            }
        ];
        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new ()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new()
                    {
                        Name = "Hot Chicken",
                        Description = "Hot Chicken (10 pcs.)",
                        Price = 9.99M,
                    },
                    new()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.99M,
                    }
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Main street 123",
                    ZipCode = "WC2N"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description = "McDonald",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new ()
                {
                    City = "London",
                    Street = "Main street 124",
                    ZipCode = "WC2N"
                }
            }
        ];
        
        return restaurants;
    }
}