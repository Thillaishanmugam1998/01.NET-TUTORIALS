namespace Routing_Parameters_and_Query_String.Models;

public class OrderRouteParameters
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string City { get; set; } = string.Empty;
}
