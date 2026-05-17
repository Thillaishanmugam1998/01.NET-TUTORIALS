using SwiggyRelated.API.Models;

namespace SwiggyRelated.API.Services;

/// <summary>
/// Defines the contract for reading and changing Swiggy order data.
/// </summary>
public interface IOrderService
{
    #region 01_ReadMethods
    /// <summary>
    /// Returns all available orders from the in-memory list.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    List<Order> GetAllOrders();

    /// <summary>
    /// Returns one order that matches the given id.
    /// </summary>
    /// <param name="id">The order id to search for.</param>
    /// <returns>The matched order, or null when no order exists.</returns>
    Order? GetOrderById(int id);

    /// <summary>
    /// Returns all orders for a specific customer.
    /// </summary>
    /// <param name="customerName">The customer name used for filtering.</param>
    /// <returns>A filtered list of orders.</returns>
    List<Order> GetOrdersByCustomer(string customerName);

    /// <summary>
    /// Returns all orders for a specific status.
    /// </summary>
    /// <param name="status">The status used for filtering.</param>
    /// <returns>A filtered list of orders.</returns>
    List<Order> GetOrdersByStatus(string status);

    /// <summary>
    /// Returns orders that match both the customer name and status from route parameters.
    /// </summary>
    /// <param name="customerName">The customer name from the route.</param>
    /// <param name="status">The status from the route.</param>
    /// <returns>A filtered list of orders.</returns>
    List<Order> GetOrdersByCustomerAndStatus(string customerName, string status);

    /// <summary>
    /// Returns all orders for a specific delivery area.
    /// </summary>
    /// <param name="area">The delivery area used for filtering.</param>
    /// <returns>A filtered list of orders.</returns>
    List<Order> SearchOrdersByArea(string area);

    /// <summary>
    /// Searches orders by using multiple optional query string values together.
    /// </summary>
    /// <param name="restaurantName">The optional restaurant name filter.</param>
    /// <param name="deliveryArea">The optional delivery area filter.</param>
    /// <param name="status">The optional status filter.</param>
    /// <param name="minAmount">The optional minimum amount filter.</param>
    /// <param name="maxAmount">The optional maximum amount filter.</param>
    /// <returns>A filtered list of orders.</returns>
    List<Order> SearchOrders(string? restaurantName, string? deliveryArea, string? status, decimal? minAmount, decimal? maxAmount);

    /// <summary>
    /// Returns a smaller page of orders by using query string values.
    /// </summary>
    /// <param name="pageNumber">The page number from the query string.</param>
    /// <param name="pageSize">The page size from the query string.</param>
    /// <returns>A paged list of orders.</returns>
    List<Order> GetPagedOrders(int pageNumber, int pageSize);

    /// <summary>
    /// Builds a simple tracking message for one order.
    /// </summary>
    /// <param name="id">The order id to track.</param>
    /// <returns>A tracking message, or null when the order does not exist.</returns>
    string? TrackOrder(int id);
    #endregion

    #region 02_WriteMethods
    /// <summary>
    /// Adds a new order to the in-memory list.
    /// </summary>
    /// <param name="order">The new order to add.</param>
    /// <returns>The saved order with its generated id.</returns>
    Order AddOrder(Order order);

    /// <summary>
    /// Updates only the status of an existing order.
    /// </summary>
    /// <param name="id">The order id to update.</param>
    /// <param name="status">The new status value.</param>
    /// <returns>True when the update succeeds; otherwise false.</returns>
    bool UpdateOrderStatus(int id, string status);

    /// <summary>
    /// Deletes an order from the in-memory list.
    /// </summary>
    /// <param name="id">The order id to remove.</param>
    /// <returns>True when the delete succeeds; otherwise false.</returns>
    bool DeleteOrder(int id);
    #endregion
}
