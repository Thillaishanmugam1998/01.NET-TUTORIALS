using SwiggyAPI.OutOfProcessHosting.API.Models;

namespace SwiggyAPI.OutOfProcessHosting.API.Interfaces;

public interface IRestaurantService
{
    List<Restaurant> GetAllRestaurants();
    Restaurant? GetRestaurantById(int restaurantId);
    List<Restaurant> GetOpenRestaurants();
    Restaurant AddRestaurant(RestaurantCreateRequest request);
    Restaurant? UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request);
    bool DeleteRestaurant(int restaurantId);
}
