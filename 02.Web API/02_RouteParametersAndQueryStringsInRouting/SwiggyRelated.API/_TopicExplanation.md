```csharp
/*
#region 01_WhatIsThisTopic
Route parameters and query strings are two ways to send values to an ASP.NET Core Web API endpoint.

Simple definition:
- Route parameters send data inside the URL path.
- Query strings send data after the ? symbol in the URL.

Swiggy analogy:
- Route parameter: "Show order 101." The order id is part of the main path.
- Query string: "Show delivered orders in RS Puram above 200 rupees." These are extra filters added to the request.
#endregion

#region 02_WhyDoWeNeedIt
Problem:
- Many API requests need input values.
- The server must know exactly which order or which filters the client wants.

Before:
- We can return only general data.
- Filtering and targeting one specific item becomes difficult.

After:
- We can get exact resources:
  GET /api/orders/1
- We can filter with flexible conditions:
  GET /api/orders/advanced-search?status=Delivered&deliveryArea=RS%20Puram

Rule of thumb:
- Route parameters are best for required values and path identity.
- Query strings are best for optional filters, search, sorting, and paging.
#endregion

#region 03_HowItWorks
Step-by-step:
1. Client sends a request URL.
2. ASP.NET Core checks the controller route pattern.
3. Route values are extracted from the URL path.
4. Query string values are extracted from the part after ?.
5. Model binding puts those values into action method parameters.
6. Controller passes those values to the service layer.
7. Service filters the static List<Order>.
8. API returns the result as JSON.

Simple diagram:
Client URL
    ->
Route Matching
    ->
Read Route Parameters + Query Strings
    ->
Controller Action
    ->
Service Layer
    ->
JSON Response
#endregion

#region 04_KeyConcepts
1. Route Parameter
Value inside the URL path.
Example:
/api/orders/5

2. Query String
Value after ? in the URL.
Example:
/api/orders/search?area=RS%20Puram

3. Route Template
Pattern written in route attributes.
Example:
[HttpGet("{id:int}")]

4. Route Constraint
Validation rule for route values.
Examples:
- {id:int}
- {status:alpha}

5. Model Binding
ASP.NET Core automatically reads incoming values and places them into method parameters.

6. Optional Filter
Query strings allow optional values.
The client can send one filter, many filters, or none.

7. Paging
Query strings commonly carry pageNumber and pageSize values.
#endregion

#region 05_RealWorldExample
Swiggy examples:

1. One order by id
GET /api/orders/2
Support team wants one exact order.

2. Orders by customer and status
GET /api/orders/customer/Asha/status/Placed
Support team wants Asha's placed orders only.

3. Orders by area
GET /api/orders/search?area=RS%20Puram
Show orders going to RS Puram.

4. Advanced search
GET /api/orders/advanced-search?restaurantName=Annapoorna&deliveryArea=Gandhipuram&status=Placed&minAmount=200&maxAmount=300
Show Annapoorna orders in Gandhipuram with Placed status and amount between 200 and 300.

5. Paged orders
GET /api/orders/paged?pageNumber=1&pageSize=2
Return only a small chunk of orders at one time.
#endregion

#region 06_CodeWalkthrough
Program.cs
- Remains the same as the previous topic.
- It still adds controllers, OpenAPI, and the service registration.

Services/IOrderService.cs
- Added a method for multiple route parameters:
  GetOrdersByCustomerAndStatus
- Added a method for multiple query string filters:
  SearchOrders
- Added a method for paging:
  GetPagedOrders

Services/OrderService.cs
- Implements combined customer + status filtering.
- Implements advanced optional filtering using query strings.
- Implements paging with Skip and Take.

Controllers/OrdersController.cs
- Keeps earlier routing examples from topic 1.
- Adds a route with multiple route parameters:
  customer/{customerName}/status/{status}
- Adds advanced query string filtering:
  advanced-search
- Adds paging through query strings:
  paged

SwiggyRelated.API.http
- Added request samples for:
  multiple route parameters
  advanced query string search
  paged query string example

_TopicExplanation.md
- Explains this full topic in easy English using C# comment style inside Markdown.
#endregion

#region 07_CommonMistakes
1. Using route parameters for too many optional filters
This makes URLs hard to read.

2. Using query strings for main resource identity
/api/orders/5 is usually clearer than /api/orders?id=5 for a single resource.

3. Forgetting route constraints
If id should be a number, use {id:int}.

4. Not validating query values
Paging values like pageNumber and pageSize should be greater than 0.

5. Mixing up path data and filter data
Path usually shows "what resource".
Query string usually shows "how to filter it".

6. Not using clear parameter names
Parameter names should clearly match the route placeholders and query meaning.
#endregion

#region 08_QuickSummary
Route parameters and query strings help ASP.NET Core Web API receive values from the URL. Route parameters are best for required path-based values like order id or customer name, while query strings are best for optional filters such as area, status, amount range, and paging. In this topic, we extended the Swiggy project with multiple route parameters, advanced query string filters, and paged results while keeping the same simple controller -> interface -> service structure.
#endregion
*/
```
