using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.API.Interfaces;
using SwiggyAPI.API.Models;

namespace SwiggyAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    #region --- 01. constructor injection ---
    public RestaurantsController(IRestaurantService restaurantService)
    {
        // ASP.NET Core gives the service object automatically using Dependency Injection.
        _restaurantService = restaurantService;
    }
    #endregion

    #region --- 02. Get all restaurants ---
    [HttpGet]
    public ActionResult<List<Restaurant>> GetRestaurants()
    {
        // This method calls the service layer and returns all restaurants.
        var restaurants = _restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }
    #endregion

    #region --- 03. Get restaurant by Id ---
    [HttpGet("{restaurantId:int}")]
    public ActionResult<Restaurant> GetRestaurantById(int restaurantId)
    {
        // Route value restaurantId comes from the URL: /api/restaurants/1
        var restaurant = _restaurantService.GetRestaurantById(restaurantId);

        if (restaurant is null)
        {
            return NotFound($"Restaurant with Id {restaurantId} was not found.");
        }

        return Ok(restaurant);
    }
    #endregion

}
