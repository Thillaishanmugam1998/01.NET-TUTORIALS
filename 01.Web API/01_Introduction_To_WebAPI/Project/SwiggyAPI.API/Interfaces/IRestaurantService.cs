using SwiggyAPI.API.Models;

namespace SwiggyAPI.API.Interfaces;

public interface IRestaurantService
{
    List<Restaurant> GetAllRestaurants();
    Restaurant? GetRestaurantById(int restaurantId);
}
