using SwiggyRelated.API.Models;

namespace SwiggyRelated.API.Services;

/// <summary>
/// Provides simple in-memory order operations for the Swiggy learning project.
/// </summary>
public sealed class OrderService : IOrderService
{
    #region 01_Fields
    // Static list acts like a fake database for learning basic API concepts.
    private static readonly List<Order> Orders =
    [
        new Order { Id = 1, CustomerName = "Asha", RestaurantName = "Annapoorna", DeliveryArea = "Gandhipuram", Status = "Placed", TotalAmount = 250 },
        new Order { Id = 2, CustomerName = "Ravi", RestaurantName = "Burger Hub", DeliveryArea = "RS Puram", Status = "Preparing", TotalAmount = 180 },
        new Order { Id = 3, CustomerName = "Meena", RestaurantName = "Dosa Corner", DeliveryArea = "Saibaba Colony", Status = "Delivered", TotalAmount = 320 }
    ];
    #endregion

    #region 02_ReadMethods
    /// <summary>
    /// Returns every order from the in-memory list.
    /// </summary>
    /// <returns>A copy of the current orders.</returns>
    public List<Order> GetAllOrders()
    {
        // Return a new list so outside code does not directly modify the original static list reference.
        return [.. Orders];
    }

    /// <summary>
    /// Returns the order that matches the given id.
    /// </summary>
    /// <param name="id">The order id to search for.</param>
    /// <returns>The matched order, or null when not found.</returns>
    public Order? GetOrderById(int id)
    {
        // Find the first order whose Id matches the route value.
        return Orders.FirstOrDefault(order => order.Id == id);
    }

    /// <summary>
    /// Returns all orders placed by the given customer.
    /// </summary>
    /// <param name="customerName">The customer name used for filtering.</param>
    /// <returns>A list of matched orders.</returns>
    public List<Order> GetOrdersByCustomer(string customerName)
    {
        // Compare names in a case-insensitive way so the API is easier for beginners to test.
        return Orders
            .Where(order => order.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Returns all orders that match the given status.
    /// </summary>
    /// <param name="status">The status used for filtering.</param>
    /// <returns>A list of matched orders.</returns>
    public List<Order> GetOrdersByStatus(string status)
    {
        // Filter orders such as Placed, Preparing, or Delivered.
        return Orders
            .Where(order => order.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Returns all orders that match both the given customer and status.
    /// </summary>
    /// <param name="customerName">The customer name used for filtering.</param>
    /// <param name="status">The status used for filtering.</param>
    /// <returns>A list of matched orders.</returns>
    public List<Order> GetOrdersByCustomerAndStatus(string customerName, string status)
    {
        // This method demonstrates how multiple route parameter values can work together.
        return Orders
            .Where(order =>
                order.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase) &&
                order.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Returns all orders that belong to the given delivery area.
    /// </summary>
    /// <param name="area">The area used for filtering.</param>
    /// <returns>A list of matched orders.</returns>
    public List<Order> SearchOrdersByArea(string area)
    {
        // Query string based filtering is common for search operations.
        return Orders
            .Where(order => order.DeliveryArea.Equals(area, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Searches orders by using multiple optional query string filters.
    /// </summary>
    /// <param name="restaurantName">The optional restaurant name filter.</param>
    /// <param name="deliveryArea">The optional delivery area filter.</param>
    /// <param name="status">The optional status filter.</param>
    /// <param name="minAmount">The optional minimum amount filter.</param>
    /// <param name="maxAmount">The optional maximum amount filter.</param>
    /// <returns>A list of matched orders.</returns>
    public List<Order> SearchOrders(string? restaurantName, string? deliveryArea, string? status, decimal? minAmount, decimal? maxAmount)
    {
        // Start with all orders, then reduce the list only for filters that the client actually sends.
        IEnumerable<Order> filteredOrders = Orders;

        if (!string.IsNullOrWhiteSpace(restaurantName))
        {
            filteredOrders = filteredOrders.Where(order =>
                order.RestaurantName.Equals(restaurantName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(deliveryArea))
        {
            filteredOrders = filteredOrders.Where(order =>
                order.DeliveryArea.Equals(deliveryArea, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            filteredOrders = filteredOrders.Where(order =>
                order.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (minAmount.HasValue)
        {
            filteredOrders = filteredOrders.Where(order => order.TotalAmount >= minAmount.Value);
        }

        if (maxAmount.HasValue)
        {
            filteredOrders = filteredOrders.Where(order => order.TotalAmount <= maxAmount.Value);
        }

        return filteredOrders.ToList();
    }

    /// <summary>
    /// Returns orders page by page.
    /// </summary>
    /// <param name="pageNumber">The requested page number.</param>
    /// <param name="pageSize">The number of records per page.</param>
    /// <returns>A paged list of orders.</returns>
    public List<Order> GetPagedOrders(int pageNumber, int pageSize)
    {
        // Skip moves past earlier records, and Take returns only the requested page size.
        return Orders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    /// <summary>
    /// Creates a simple tracking message for the requested order.
    /// </summary>
    /// <param name="id">The order id to track.</param>
    /// <returns>A tracking message, or null when the order is not found.</returns>
    public string? TrackOrder(int id)
    {
        var existingOrder = GetOrderById(id);

        if (existingOrder is null)
        {
            return null;
        }

        // This message simulates the kind of text a food delivery app may show in tracking.
        return $"Order {existingOrder.Id} for {existingOrder.CustomerName} is currently {existingOrder.Status}.";
    }
    #endregion

    #region 03_WriteMethods
    /// <summary>
    /// Adds a new order to the in-memory list.
    /// </summary>
    /// <param name="order">The order payload sent by the client.</param>
    /// <returns>The saved order with a generated id.</returns>
    public Order AddOrder(Order order)
    {
        // Generate the next id manually because we are not using a database yet.
        order.Id = Orders.Count == 0 ? 1 : Orders.Max(existingOrder => existingOrder.Id) + 1;

        Orders.Add(order);
        return order;
    }

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    /// <param name="id">The order id to update.</param>
    /// <param name="status">The new status value.</param>
    /// <returns>True when the update succeeds; otherwise false.</returns>
    public bool UpdateOrderStatus(int id, string status)
    {
        var existingOrder = GetOrderById(id);

        if (existingOrder is null)
        {
            return false;
        }

        // Change only the status to keep the example focused on routing.
        existingOrder.Status = status;
        return true;
    }

    /// <summary>
    /// Deletes an existing order from the in-memory list.
    /// </summary>
    /// <param name="id">The order id to remove.</param>
    /// <returns>True when the delete succeeds; otherwise false.</returns>
    public bool DeleteOrder(int id)
    {
        var existingOrder = GetOrderById(id);

        if (existingOrder is null)
        {
            return false;
        }

        // Remove the matched object from the list.
        Orders.Remove(existingOrder);
        return true;
    }
    #endregion
}
