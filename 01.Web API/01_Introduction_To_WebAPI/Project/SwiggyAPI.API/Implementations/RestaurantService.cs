using SwiggyAPI.API.Interfaces;
using SwiggyAPI.API.Models;

namespace SwiggyAPI.API.Implementations;

// This service returns restaurant data.
public class RestaurantService : IRestaurantService
{
    #region --- 01. Static list of restaurants ---
    // Temporary static list for learning purpose.
    private static readonly List<Restaurant> Restaurants =
    [
        new Restaurant
        {
            Id = 1,
            Name = "A2B Veg Delight",
            City = "Chennai",
            Cuisine = "South Indian",
            Rating = 4.5m,
            IsOpen = true
        },
        new Restaurant
        {
            Id = 2,
            Name = "Burger Street",
            City = "Bengaluru",
            Cuisine = "Fast Food",
            Rating = 4.2m,
            IsOpen = true
        },
        new Restaurant
        {
            Id = 3,
            Name = "Spice Garden",
            City = "Hyderabad",
            Cuisine = "Biryani",
            Rating = 4.7m,
            IsOpen = false
        }
    ];
    #endregion

    #region --- 02. Methods to retrieve restaurant data ---
    public List<Restaurant> GetAllRestaurants()
    {
        return Restaurants;
    }
    #endregion

    #region --- 03. Method to retrieve a restaurant by its ID ---
    public Restaurant? GetRestaurantById(int restaurantId)
    {
        // LINQ helps us search the list using a simple condition.
        return Restaurants.FirstOrDefault(currentRestaurant => currentRestaurant.Id == restaurantId);
    }
    #endregion
}
