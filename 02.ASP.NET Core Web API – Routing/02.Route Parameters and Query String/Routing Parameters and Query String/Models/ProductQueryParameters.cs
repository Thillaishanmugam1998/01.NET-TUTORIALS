namespace Routing_Parameters_and_Query_String.Models;

public class ProductQueryParameters
{
    public int ProductId { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;
}
