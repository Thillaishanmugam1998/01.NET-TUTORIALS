using SwiggyAPI.Controllers.API.Implementations;
using SwiggyAPI.Controllers.API.Interfaces;

namespace SwiggyAPI.Controllers.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        return services;
    }
}
