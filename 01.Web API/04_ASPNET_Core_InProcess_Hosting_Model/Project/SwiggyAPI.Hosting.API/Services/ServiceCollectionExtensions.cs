using SwiggyAPI.Hosting.API.Implementations;
using SwiggyAPI.Hosting.API.Interfaces;

namespace SwiggyAPI.Hosting.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        // Service registration tells DI which class to create for IHostingInfoService.
        services.AddScoped<IHostingInfoService, HostingInfoService>();

        return services;
    }
}
