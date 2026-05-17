using SwiggyAPI.Swagger.API.Implementations;
using SwiggyAPI.Swagger.API.Interfaces;

namespace SwiggyAPI.Swagger.API.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service registration tells DI which class to create for IRestaurantService.
        services.AddScoped<IRestaurantService, RestaurantService>();

        // Transient creates a new object every time it is requested.
        services.AddTransient<ITransientIdService, TransientIdService>();

        // Scoped creates one object per HTTP request.
        services.AddScoped<IScopedIdService, ScopedIdService>();

        // Singleton creates one object for the full application lifetime.
        services.AddSingleton<ISingletonIdService, SingletonIdService>();

        // This scoped service collects lifetime ids within the same HTTP request.
        services.AddScoped<ILifetimeReportService, LifetimeReportService>();

        return services;
    }
}
