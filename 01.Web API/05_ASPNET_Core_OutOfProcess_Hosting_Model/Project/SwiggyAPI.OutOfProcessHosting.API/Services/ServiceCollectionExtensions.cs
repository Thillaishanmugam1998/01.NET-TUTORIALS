using SwiggyAPI.OutOfProcessHosting.API.Implementations;
using SwiggyAPI.OutOfProcessHosting.API.Interfaces;

namespace SwiggyAPI.OutOfProcessHosting.API.Services;

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
