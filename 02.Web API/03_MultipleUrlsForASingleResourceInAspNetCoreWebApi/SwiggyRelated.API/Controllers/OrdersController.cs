using Microsoft.AspNetCore.Mvc;
using SwiggyRelated.API.Models;
using SwiggyRelated.API.Services;

namespace SwiggyRelated.API.Controllers;

/// <summary>
/// Exposes routing examples for managing Swiggy orders.
/// </summary>
[ApiController]
[Route("api/orders")]
public sealed class OrdersController : ControllerBase
{
    #region 01_FieldsAndConstructor
    private readonly IOrderService _orderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/> class.
    /// </summary>
    /// <param name="orderService">The service used to read and change order data.</param>
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    #endregion

    #region 02_GetRoutes
    /// <summary>
    /// Returns all orders.
    /// </summary>
    /// <returns>A list of all current orders.</returns>
    [HttpGet]
    public ActionResult<List<Order>> GetAllOrders()
    {
        // This route matches GET /api/orders.
        return Ok(_orderService.GetAllOrders());
    }

    /// <summary>
    /// Returns one order by id.
    /// </summary>
    /// <param name="id">The order id coming from the URL path.</param>
    /// <returns>The matched order when found.</returns>
    [HttpGet("{id:int}")]
    [HttpGet("details/{id:int}")]
    [HttpGet("order-details/{id:int}")]
    public ActionResult<Order> GetOrderById(int id)
    {
        // These three route templates point to the same action, so one resource can be reached by multiple URLs.
        // Example URLs:
        // GET /api/orders/1
        // GET /api/orders/details/1
        // GET /api/orders/order-details/1
        var order = _orderService.GetOrderById(id);

        if (order is null)
        {
            return NotFound($"Order with id {id} was not found.");
        }

        return Ok(order);
    }

    /// <summary>
    /// Returns orders for a given customer.
    /// </summary>
    /// <param name="customerName">The customer name coming from the route.</param>
    /// <returns>A filtered order list.</returns>
    [HttpGet("customer/{customerName}")]
    public ActionResult<List<Order>> GetOrdersByCustomer(string customerName)
    {
        // This route shows how a text value can be passed inside the URL.
        return Ok(_orderService.GetOrdersByCustomer(customerName));
    }

    /// <summary>
    /// Returns orders for a given status.
    /// </summary>
    /// <param name="status">The order status coming from the route.</param>
    /// <returns>A filtered order list.</returns>
    [HttpGet("status/{status:alpha}")]
    public ActionResult<List<Order>> GetOrdersByStatus(string status)
    {
        // The :alpha constraint allows only alphabet characters in the route segment.
        return Ok(_orderService.GetOrdersByStatus(status));
    }

    /// <summary>
    /// Returns orders by using two route parameters together.
    /// </summary>
    /// <param name="customerName">The customer name coming from the route.</param>
    /// <param name="status">The status coming from the route.</param>
    /// <returns>A filtered order list.</returns>
    [HttpGet("customer/{customerName}/status/{status:alpha}")]
    public ActionResult<List<Order>> GetOrdersByCustomerAndStatus(string customerName, string status)
    {
        // This route shows how multiple route parameter values can be passed inside one URL path.
        return Ok(_orderService.GetOrdersByCustomerAndStatus(customerName, status));
    }

    /// <summary>
    /// Searches orders by delivery area using a query string.
    /// </summary>
    /// <param name="area">The delivery area passed in the query string.</param>
    /// <returns>A filtered order list.</returns>
    [HttpGet("search")]
    public ActionResult<List<Order>> SearchOrdersByArea([FromQuery] string area)
    {
        // This route matches GET /api/orders/search?area=RS%20Puram.
        return Ok(_orderService.SearchOrdersByArea(area));
    }

    /// <summary>
    /// Searches orders by using multiple optional query string values.
    /// </summary>
    /// <param name="restaurantName">The restaurant name from the query string.</param>
    /// <param name="deliveryArea">The delivery area from the query string.</param>
    /// <param name="status">The status from the query string.</param>
    /// <param name="minAmount">The minimum amount from the query string.</param>
    /// <param name="maxAmount">The maximum amount from the query string.</param>
    /// <returns>A filtered order list.</returns>
    [HttpGet("advanced-search")]
    public ActionResult<List<Order>> SearchOrders(
        [FromQuery] string? restaurantName,
        [FromQuery] string? deliveryArea,
        [FromQuery] string? status,
        [FromQuery] decimal? minAmount,
        [FromQuery] decimal? maxAmount)
    {
        // Query strings are best when filters are optional and can appear in different combinations.
        return Ok(_orderService.SearchOrders(restaurantName, deliveryArea, status, minAmount, maxAmount));
    }

    /// <summary>
    /// Returns orders page by page using query string values.
    /// </summary>
    /// <param name="pageNumber">The page number from the query string.</param>
    /// <param name="pageSize">The page size from the query string.</param>
    /// <returns>A paged order list.</returns>
    [HttpGet("paged")]
    public ActionResult<List<Order>> GetPagedOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2)
    {
        // We validate paging values so the endpoint stays predictable for beginners.
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("pageNumber and pageSize must be greater than 0.");
        }

        return Ok(_orderService.GetPagedOrders(pageNumber, pageSize));
    }

    /// <summary>
    /// Returns a simple tracking message for one order.
    /// </summary>
    /// <param name="id">The order id coming from the route.</param>
    /// <returns>A tracking message when the order exists.</returns>
    [HttpGet("{id:int}/track")]
    [HttpGet("track-order/{id:int}")]
    public ActionResult<string> TrackOrder(int id)
    {
        // Both URLs below return the same tracking resource for the same order:
        // GET /api/orders/1/track
        // GET /api/orders/track-order/1
        var trackingMessage = _orderService.TrackOrder(id);

        if (trackingMessage is null)
        {
            return NotFound($"Order with id {id} was not found.");
        }

        return Ok(trackingMessage);
    }
    #endregion

    #region 03_WriteRoutes
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order body sent by the client.</param>
    /// <returns>The created order with its generated id.</returns>
    [HttpPost]
    public ActionResult<Order> AddOrder([FromBody] Order order)
    {
        // POST /api/orders creates a new resource in our in-memory list.
        var createdOrder = _orderService.AddOrder(order);

        // CreatedAtAction returns HTTP 201 and also tells the client where to fetch the new resource.
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    /// <summary>
    /// Updates only the status of an order.
    /// </summary>
    /// <param name="id">The order id coming from the route.</param>
    /// <param name="status">The new status coming from the route.</param>
    /// <returns>A success message when the update works.</returns>
    [HttpPut("{id:int}/status/{status:alpha}")]
    public ActionResult UpdateOrderStatus(int id, string status)
    {
        // This route demonstrates multiple route parameters in one URL.
        var isUpdated = _orderService.UpdateOrderStatus(id, status);

        if (!isUpdated)
        {
            return NotFound($"Order with id {id} was not found.");
        }

        return Ok($"Order {id} status updated to {status}.");
    }

    /// <summary>
    /// Deletes an order by id.
    /// </summary>
    /// <param name="id">The order id coming from the route.</param>
    /// <returns>A success message when the delete works.</returns>
    [HttpDelete("{id:int}")]
    public ActionResult DeleteOrder(int id)
    {
        // DELETE /api/orders/3 removes the matched order from the list.
        var isDeleted = _orderService.DeleteOrder(id);

        if (!isDeleted)
        {
            return NotFound($"Order with id {id} was not found.");
        }

        return Ok($"Order {id} deleted successfully.");
    }
    #endregion
}
