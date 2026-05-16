namespace SwiggyAPI.Controllers.API.Models;

public class RestaurantUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Cuisine { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public bool IsOpen { get; set; }
}
