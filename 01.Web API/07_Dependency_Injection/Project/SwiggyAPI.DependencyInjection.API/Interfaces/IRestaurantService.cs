using SwiggyAPI.DependencyInjection.API.Models;

namespace SwiggyAPI.DependencyInjection.API.Interfaces;

public interface IRestaurantService
{
    List<Restaurant> GetAllRestaurants();
    Restaurant? GetRestaurantById(int restaurantId);
    List<Restaurant> GetOpenRestaurants();
    string GetRestaurantModuleMessage();
    Restaurant AddRestaurant(RestaurantCreateRequest request);
    Restaurant? UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request);
    bool DeleteRestaurant(int restaurantId);
}
