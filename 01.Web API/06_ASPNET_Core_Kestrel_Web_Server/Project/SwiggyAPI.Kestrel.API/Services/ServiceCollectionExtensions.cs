using SwiggyAPI.Kestrel.API.Implementations;
using SwiggyAPI.Kestrel.API.Interfaces;

namespace SwiggyAPI.Kestrel.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        // Service registration tells DI which class to create for IServerInfoService.
        services.AddScoped<IServerInfoService, ServerInfoService>();

        return services;
    }
}
