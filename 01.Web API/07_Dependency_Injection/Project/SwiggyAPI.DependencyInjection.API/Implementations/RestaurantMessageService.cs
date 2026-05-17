using SwiggyAPI.DependencyInjection.API.Interfaces;

namespace SwiggyAPI.DependencyInjection.API.Implementations;

// This service returns a simple message for restaurant module.
public class RestaurantMessageService : IRestaurantMessageService
{
    public string GetWelcomeMessage()
    {
        return "Restaurant service is working through Dependency Injection.";
    }
}
