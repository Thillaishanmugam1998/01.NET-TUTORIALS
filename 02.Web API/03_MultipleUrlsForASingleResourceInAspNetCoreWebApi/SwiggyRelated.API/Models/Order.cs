namespace SwiggyRelated.API.Models;

/// <summary>
/// Represents a simple food order in our Swiggy learning project.
/// </summary>
public sealed class Order
{
    #region 01_Properties
    /// <summary>
    /// Gets or sets the unique order id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the customer name who placed the order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the restaurant name from which the order was placed.
    /// </summary>
    public string RestaurantName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the delivery area for the order.
    /// </summary>
    public string DeliveryArea { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current order status.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total bill amount for the order.
    /// </summary>
    public decimal TotalAmount { get; set; }
    #endregion
}
