using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.HttpBasics.API.Interfaces;
using SwiggyAPI.HttpBasics.API.Models;

namespace SwiggyAPI.HttpBasics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    #region --- Information ---

    /*
     * | HTTP Method | Usually Used For | Data Usually Comes From | Example URL          | Body Used?   |
       | ----------- | ---------------- | ----------------------- | -------------------- | ------------ |
       | GET         | Read/Get data    | Route / Query String    | `/api/restaurants/5` | ❌ Usually No |
       | POST        | Create new data  | Body                    | `/api/restaurants`   | ✅ Yes        |
       | PUT         | Full update      | Body + Route            | `/api/restaurants/5` | ✅ Yes        |
       | PATCH       | Partial update   | Body + Route            | `/api/restaurants/5` | ✅ Yes        |
       | DELETE      | Delete data      | Route                   | `/api/restaurants/5` | ❌ Usually No |

     */

    #endregion


    #region --- 01. Contructor Injection ---
    public RestaurantsController(IRestaurantService restaurantService)
    {
        // Dependency Injection gives the required service object automatically.
        _restaurantService = restaurantService;
    }
    #endregion

    #region --- 02. Action Methods ---
    [HttpGet]
    public ActionResult<List<Restaurant>> GetRestaurants()
    {
        // HTTP GET is used to read data from the server.
        var restaurants = _restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }
    #endregion

    #region --- 03. Route Parameters ---
    [HttpGet("{restaurantId:int}")]
    public ActionResult<Restaurant> GetRestaurantById(int restaurantId)
    {
        // Route value comes from URL: /api/restaurants/1
        var restaurant = _restaurantService.GetRestaurantById(restaurantId);

        if (restaurant is null)
        {
            return NotFound($"Restaurant with Id {restaurantId} was not found.");
        }

        return Ok(restaurant);
    }
    #endregion

    #region --- 04. Query Parameters ---
    [HttpPost]
    public ActionResult<Restaurant> AddRestaurant(RestaurantCreateRequest request)
    {
        // HTTP POST is used to create new data on the server.
        var createdRestaurant = _restaurantService.AddRestaurant(request);

        return CreatedAtAction(nameof(GetRestaurantById), new { restaurantId = createdRestaurant.Id }, createdRestaurant);
    }
    #endregion

    #region --- 05. Request Body ---
    [HttpPut("{restaurantId:int}")]
    public ActionResult<Restaurant> UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request)
    {
        // HTTP PUT is used to fully update an existing record.
        var updatedRestaurant = _restaurantService.UpdateRestaurant(restaurantId, request);

        if (updatedRestaurant is null)
        {
            return NotFound($"Restaurant with Id {restaurantId} was not found.");
        }

        return Ok(updatedRestaurant);
    }
    #endregion

    #region --- 06. HTTP DELETE ---
    [HttpDelete("{restaurantId:int}")]
    public ActionResult DeleteRestaurant(int restaurantId)
    {
        // HTTP DELETE is used to remove data from the server.
        var isDeleted = _restaurantService.DeleteRestaurant(restaurantId);

        if (!isDeleted)
        {
            return NotFound($"Restaurant with Id {restaurantId} was not found.");
        }

        return Ok($"Restaurant with Id {restaurantId} deleted successfully.");
    }
    #endregion

}
