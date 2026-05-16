using SwiggyAPI.HttpBasics.API.Models;

namespace SwiggyAPI.HttpBasics.API.Interfaces;

public interface IRestaurantService
{
    List<Restaurant> GetAllRestaurants();
    Restaurant? GetRestaurantById(int restaurantId);
    Restaurant AddRestaurant(RestaurantCreateRequest request);
    Restaurant? UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request);
    bool DeleteRestaurant(int restaurantId);
}
