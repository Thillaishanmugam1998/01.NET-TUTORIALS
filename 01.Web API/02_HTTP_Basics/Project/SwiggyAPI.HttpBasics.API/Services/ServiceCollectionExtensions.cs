using SwiggyAPI.HttpBasics.API.Implementations;
using SwiggyAPI.HttpBasics.API.Interfaces;

namespace SwiggyAPI.HttpBasics.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        return services;
    }
}
