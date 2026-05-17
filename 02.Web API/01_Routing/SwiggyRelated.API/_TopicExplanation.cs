namespace SwiggyRelated.API;

/*
#region 01_WhatIsThisTopic
Routing in ASP.NET Core Web API is the feature that decides which controller action method should run
when a request comes to the application.

Simple definition:
Routing is like a matching system. The URL and HTTP method sent by the client are matched to the correct
controller method in your API.

Swiggy analogy:
Think about a Swiggy call center.
- If a customer says "show all my orders", the call goes to one team.
- If a customer says "track order 101", the call goes to another team.
- If a customer says "cancel order 101", the call goes to another team.

In the same way, routing sends each API request to the correct method.
#endregion

#region 02_WhyDoWeNeedIt
Problem without routing:
- The application receives a request.
- But it does not know which method should handle it.
- All requests would become confusing and mixed together.

Problem routing solves:
- It creates a clear path from request URL to action method.
- It helps organize endpoints in a readable way.
- It allows us to pass values like id, status, or customer name through the URL.

Before:
- No clear mapping between URL and code.
- Hard to understand which method runs.

After:
- URL clearly points to one action.
- Example:
  GET /api/orders
  GET /api/orders/1
  GET /api/orders/customer/Asha
  PUT /api/orders/1/status/Delivered
#endregion

#region 03_HowItWorks
Step-by-step:
1. The client sends an HTTP request.
2. ASP.NET Core reads the request URL and HTTP method.
3. It checks controller route attributes such as [Route], [HttpGet], [HttpPost], [HttpPut], and [HttpDelete].
4. It finds the action method whose route pattern matches the request.
5. It reads route values like id or status and passes them into method parameters.
6. The controller calls the service layer.
7. The service processes data from the static List<Order>.
8. The controller returns the response.

Simple flow diagram:
Client Request
    ->
Routing System
    ->
OrdersController Action
    ->
IOrderService
    ->
OrderService
    ->
Response
#endregion

#region 04_KeyConcepts
Important terms:

1. Route
The URL pattern used to reach an action method.
Example: /api/orders

2. Route Template
The route format written in attributes.
Example: [HttpGet("{id:int}")]

3. Route Parameter
A value coming from the URL path.
Example: in /api/orders/5, the value 5 is the route parameter.

4. Route Constraint
Rules for route values.
Example:
- {id:int} means only integer values are allowed.
- {status:alpha} means only letters are allowed.

5. Query String
Extra values sent after ? in the URL.
Example: /api/orders/search?area=RS%20Puram

6. Attribute Routing
Routing defined using attributes directly on controller classes and methods.
Example:
- [Route("api/orders")]
- [HttpGet("customer/{customerName}")]

7. HTTP Method
The operation type:
- GET = read data
- POST = create data
- PUT = update data
- DELETE = remove data
#endregion

#region 05_RealWorldExample
Swiggy use case walkthrough:

1. Show all orders
Request:
GET /api/orders
Meaning:
Swiggy admin wants to see every order in the system.

2. Show one order
Request:
GET /api/orders/2
Meaning:
Swiggy support wants details of order id 2.

3. Filter by customer
Request:
GET /api/orders/customer/Asha
Meaning:
Show only orders placed by Asha.

4. Filter by status
Request:
GET /api/orders/status/Preparing
Meaning:
Show orders that are currently being prepared.

5. Search by area
Request:
GET /api/orders/search?area=RS%20Puram
Meaning:
Show orders going to RS Puram.

6. Track an order
Request:
GET /api/orders/1/track
Meaning:
The customer wants to know where the order is in the delivery process.

7. Update status
Request:
PUT /api/orders/1/status/Delivered
Meaning:
Swiggy delivery system marks order 1 as delivered.

8. Delete an order
Request:
DELETE /api/orders/3
Meaning:
Remove a wrong or test order from the list.
#endregion

#region 06_CodeWalkthrough
Program.cs
- Creates the WebApplication builder.
- Registers controllers so controller routing works.
- Registers OpenAPI for endpoint testing and learning.
- Registers IOrderService -> OrderService in dependency injection.
- Builds the app.
- Maps controllers using app.MapControllers().

Models/Order.cs
- Defines the Order model.
- This is the shape of our order data.
- Contains fields like Id, CustomerName, RestaurantName, DeliveryArea, Status, and TotalAmount.

Services/IOrderService.cs
- Defines the contract for order operations.
- This keeps the controller dependent on an interface, not on a concrete class.
- Good for clean architecture and easy learning structure.

Services/OrderService.cs
- Stores data in a static List<Order>.
- Performs all read and write operations.
- Contains filtering logic for customer, status, and area.
- Contains add, update, and delete logic.

Controllers/OrdersController.cs
- [Route("api/orders")] sets the base route.
- [HttpGet] maps GET /api/orders.
- [HttpGet("{id:int}")] maps GET /api/orders/1.
- [HttpGet("customer/{customerName}")] maps customer-based route.
- [HttpGet("status/{status:alpha}")] shows route constraint usage.
- [HttpGet("search")] demonstrates query string use with [FromQuery].
- [HttpGet("{id:int}/track")] demonstrates nested resource/action routing.
- [HttpPost] creates an order.
- [HttpPut("{id:int}/status/{status:alpha}")] updates order status.
- [HttpDelete("{id:int}")] deletes an order.

Inline comments
- Every important method contains comments explaining what the route does and why it exists.
- This helps a beginner connect URL patterns to controller behavior.
#endregion

#region 07_CommonMistakes
Common beginner mistakes:

1. Forgetting app.MapControllers()
If this is missing, controller routes will not work.

2. Forgetting builder.Services.AddControllers()
The app needs controller support registered first.

3. Wrong route template
Example:
- Writing [HttpGet("id")] instead of [HttpGet("{id}")]
- "id" means fixed text
- "{id}" means dynamic value

4. Confusing route parameter and query string
- Route parameter: /api/orders/1
- Query string: /api/orders/search?area=RS%20Puram

5. Using wrong HTTP verb
- GET should not create data
- POST should create
- PUT should update
- DELETE should remove

6. Route conflicts
If two routes look too similar, ASP.NET Core may not know which one to use.
Route constraints like :int help avoid confusion.

7. Not matching parameter names
The route placeholder name and method parameter name should match clearly.
Example:
[HttpGet("{id:int}")]
public ActionResult GetOrderById(int id)
#endregion

#region 08_QuickSummary
Routing is the feature that maps incoming HTTP requests to the correct controller methods. In this topic,
we used attribute routing to build clear URLs for Swiggy order operations such as reading all orders,
getting one order by id, filtering by customer or status, tracking an order, creating an order, updating
status, and deleting an order. We also learned route parameters, route constraints, query strings, and
how controllers call services through an interface-based structure.
#endregion
*/
