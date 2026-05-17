using SwiggyAPI.Hosting.API.Interfaces;
using SwiggyAPI.Hosting.API.Models;

namespace SwiggyAPI.Hosting.API.Implementations;

// This service returns restaurant data.
public class RestaurantService : IRestaurantService
{
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

    public List<Restaurant> GetAllRestaurants()
    {
        return Restaurants;
    }

    public Restaurant? GetRestaurantById(int restaurantId)
    {
        // LINQ helps us search the list using a simple condition.
        return Restaurants.FirstOrDefault(currentRestaurant => currentRestaurant.Id == restaurantId);
    }

    public List<Restaurant> GetOpenRestaurants()
    {
        // LINQ filters only restaurants that are currently open.
        return Restaurants.Where(currentRestaurant => currentRestaurant.IsOpen).ToList();
    }

    public Restaurant AddRestaurant(RestaurantCreateRequest request)
    {
        var nextId = Restaurants.Max(currentRestaurant => currentRestaurant.Id) + 1;

        var restaurant = new Restaurant
        {
            Id = nextId,
            Name = request.Name,
            City = request.City,
            Cuisine = request.Cuisine,
            Rating = request.Rating,
            IsOpen = request.IsOpen
        };

        Restaurants.Add(restaurant);
        return restaurant;
    }

    public Restaurant? UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request)
    {
        var restaurant = Restaurants.FirstOrDefault(currentRestaurant => currentRestaurant.Id == restaurantId);

        if (restaurant is null)
        {
            return null;
        }

        restaurant.Name = request.Name;
        restaurant.City = request.City;
        restaurant.Cuisine = request.Cuisine;
        restaurant.Rating = request.Rating;
        restaurant.IsOpen = request.IsOpen;

        return restaurant;
    }

    public bool DeleteRestaurant(int restaurantId)
    {
        var restaurant = Restaurants.FirstOrDefault(currentRestaurant => currentRestaurant.Id == restaurantId);

        if (restaurant is null)
        {
            return false;
        }

        Restaurants.Remove(restaurant);
        return true;
    }
}
