namespace Restaurants.Infrastructure.Authorization.Constants;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string CreatedAtLeast2Restaurants = "CreatedAtLeast2Restaurants";
}