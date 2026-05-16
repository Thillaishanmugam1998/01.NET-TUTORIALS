using SwiggyAPI.API.Implementations;
using SwiggyAPI.API.Interfaces;

namespace SwiggyAPI.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // This registers: "Whenever someone asks for IRestaurantService, give them RestaurantService"
        services.AddScoped<IRestaurantService, RestaurantService>();

        return services; // Return so more methods can be chained
    }
}
