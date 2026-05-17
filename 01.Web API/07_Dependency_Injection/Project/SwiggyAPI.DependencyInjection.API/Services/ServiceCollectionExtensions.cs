using SwiggyAPI.DependencyInjection.API.Implementations;
using SwiggyAPI.DependencyInjection.API.Interfaces;

namespace SwiggyAPI.DependencyInjection.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        // Service registration tells DI which class to create for IRequestInfoService.
        services.AddScoped<IRequestInfoService, RequestInfoService>();

        // Service registration tells DI which class to create for IRestaurantMessageService.
        services.AddScoped<IRestaurantMessageService, RestaurantMessageService>();

        return services;
    }
}
