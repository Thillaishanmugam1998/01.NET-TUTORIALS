# Project Structure and Files - Controllers

## 1. Topic Overview

A controller is the entry point of a Web API request.

In simple words:

- client sends request
- controller receives request
- controller calls service
- controller returns response

In our `SwiggyAPI` project, the `Controllers` folder contains classes that expose API endpoints like:

- `GET /api/restaurants`
- `GET /api/restaurants/1`
- `POST /api/restaurants`

So if someone asks, "Which file talks directly to the client request?" the answer is: the controller.

## 2. Why It Is Used

We use controllers because they:

- receive HTTP requests
- match URL routes
- read route values and request body
- call business logic
- return status codes and JSON response

Without controllers, our API does not know how to respond to incoming URLs.

## 3. Real-Time Example

Think about Swiggy mobile app.

When user opens restaurant list, frontend sends:

```http
GET /api/restaurants
```

When admin wants only open restaurants:

```http
GET /api/restaurants/open
```

When admin adds a new restaurant:

```http
POST /api/restaurants
```

The controller handles each of these requests and connects them to the service layer.

## 4. Architecture Flow

```text
Client / Postman / Mobile App
          |
          v
RestaurantsController
          |
          v
IRestaurantService
          |
          v
RestaurantService
          |
          v
Static Restaurant List
          |
          v
JSON Response
```

## 5. Folder Structure

```text
03_Project_Structure_And_Files_Controllers/
  Project/
    SwiggyAPI.Controllers.API/
      Controllers/
        RestaurantsController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
      Interfaces/
        IRestaurantService.cs
      Implementations/
        RestaurantService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.Controllers.API.http
      SwiggyAPI.Controllers.API.csproj
  Explanation_Topic/
    Project_Structure_And_Files_Controllers_Explanation.md
  Interview_QA/
    Project_Structure_And_Files_Controllers_Interview_QA.md
  Exercises/
    Project_Structure_And_Files_Controllers_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Create `Controllers` folder

This folder stores all API controllers.

Right now we created:

- `RestaurantsController.cs`

Later in the same solution we can continue with:

- `FoodItemsController.cs`
- `OrdersController.cs`
- `CartController.cs`

### Step 2: Create controller class

We created:

```csharp
public class RestaurantsController : ControllerBase
```

`ControllerBase` gives ready-made helper methods like:

- `Ok()`
- `NotFound()`
- `CreatedAtAction()`

### Step 3: Add controller attributes

We used:

```csharp
[ApiController]
[Route("api/[controller]")]
```

Meaning:

- `[ApiController]` tells ASP.NET Core this class is an API controller
- `[Route("api/[controller]")]` creates route based on controller name

So `RestaurantsController` becomes:

```text
api/restaurants
```

### Step 4: Inject service into controller

Controller should not contain all business logic.

So we inject:

```csharp
IRestaurantService
```

This keeps code clean and easy to maintain.

### Step 5: Add action methods

We added:

- `GetRestaurants()`
- `GetRestaurantById(int restaurantId)`
- `GetOpenRestaurants()`
- `AddRestaurant(RestaurantCreateRequest request)`
- `UpdateRestaurant(int restaurantId, RestaurantUpdateRequest request)`
- `DeleteRestaurant(int restaurantId)`

Each action is connected to an HTTP method using attributes.

## 7. Full Code

Main learning file:

- `Controllers/RestaurantsController.cs`

Supporting files:

- `Interfaces/IRestaurantService.cs`
- `Implementations/RestaurantService.cs`
- `Program.cs`

These files work together to complete the request flow.

## 8. Code Explanation

### `[ApiController]`

Marks the class as Web API controller.

### `[Route("api/[controller]")]`

Creates route using controller name.

For `RestaurantsController`, route becomes:

```text
api/restaurants
```

### `[HttpGet]`

Matches `GET /api/restaurants`

### `[HttpGet("{restaurantId:int}")]`

Matches:

```text
GET /api/restaurants/1
```

`int` means route value must be integer.

### `[HttpGet("open")]`

Matches:

```text
GET /api/restaurants/open
```

This is a custom route segment.

### `ActionResult<T>`

This lets controller return:

- data
- status code
- error response

Example:

- `Ok(restaurants)`
- `NotFound("message")`

## 9. Request Lifecycle

### Example: `GET /api/restaurants/open`

1. Client sends request to `/api/restaurants/open`
2. ASP.NET Core checks all controller routes
3. It finds `RestaurantsController`
4. It finds `[HttpGet("open")]`
5. That action calls `_restaurantService.GetOpenRestaurants()`
6. Service filters the static list
7. Controller returns `200 OK`
8. Client receives JSON array

### Example: `POST /api/restaurants`

1. Client sends POST request with JSON body
2. ASP.NET Core converts JSON into `RestaurantCreateRequest`
3. Controller receives request model
4. Controller calls service
5. Service creates restaurant object
6. Controller returns `201 Created`

## 10. Interview Questions

### Beginner

What is a controller in ASP.NET Core Web API?

Controller is the class that receives HTTP requests and returns HTTP responses.

### Intermediate

What is the purpose of `[ApiController]`?

It marks the class as an API controller and enables API-friendly behavior.

### Advanced

Why should business logic not be written directly inside controller?

Because controller should stay thin and focus only on request handling, routing, and response creation.

### Real-time scenario

If route `/api/restaurants/open` is not hitting the action, what should you check?

Check:

- route attribute
- HTTP method
- controller naming
- `app.MapControllers()`
- whether app is running on expected URL

## 11. Exercises

1. Run `GET /api/restaurants` and identify which controller action is executed.
2. Run `GET /api/restaurants/open` and explain why it does not call `GetRestaurantById`.
3. Add one more custom route named `closed`.
4. Change one action return type and observe compiler suggestions.
5. Explain the difference between route value and request body.

## 12. Common Errors

- forgetting `app.MapControllers()`
- forgetting `[HttpGet]` or `[HttpPost]`
- writing wrong route template
- placing business logic directly inside controller
- returning raw strings everywhere instead of proper status codes
- forgetting to inject the service

## 13. Debugging Tips

- put breakpoint inside controller action
- confirm request URL is correct
- verify HTTP method in Postman or `.http` file
- check whether route template matches URL
- see whether dependency injection resolved the service
- if action is not hit, first inspect `Program.cs`

## 14. Best Practices

- keep controller small and readable
- keep business logic in service class
- use meaningful action names
- use proper HTTP verb attributes
- return proper status codes
- use route constraints like `{restaurantId:int}` when needed
- do not mix database code inside controller

## 15. Summary Notes

- controller is the entry gate of API request
- `Controllers` folder stores API endpoint classes
- attributes map URL and HTTP methods
- controller calls service
- service performs logic
- controller sends final response
- thin controller is a real-time industry standard
