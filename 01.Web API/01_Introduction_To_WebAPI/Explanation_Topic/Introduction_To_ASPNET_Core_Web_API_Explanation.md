# Introduction to ASP.NET Core Web API

## 1. Topic Overview

ASP.NET Core Web API is a framework used to build backend HTTP services using C#.

In simple words:

- Frontend, mobile app, or another system sends a request
- Our API receives that request
- API processes the request
- API sends a response back, usually JSON

For `SwiggyAPI`, this backend will later handle restaurants, food items, cart, orders, users, payments, and more.

## 2. Why It Is Used

We use Web API because modern applications usually need a backend service that can:

- expose data to mobile apps and web apps
- apply business rules
- validate requests
- secure endpoints
- connect to databases later
- return standard HTTP responses

Without Web API, frontend applications would have no proper backend contract.

## 3. Real-Time Example

Imagine the Swiggy mobile app opens the restaurant listing page.

The app can call:

```http
GET /api/restaurants
```

The API returns:

```json
[
  {
    "id": 1,
    "name": "A2B Veg Delight",
    "city": "Chennai",
    "cuisine": "South Indian",
    "rating": 4.5,
    "isOpen": true
  }
]
```

This is exactly how real companies expose data between frontend and backend.

## 4. Architecture Flow

For this learning path we will use simple layered architecture:

```text
Client
  |
  v
Controller
  |
  v
IService
  |
  v
Service Implementation
  |
  v
Response to Client
```

In this first lesson:

- `RestaurantsController` handles the incoming HTTP request
- `IRestaurantService` defines what the service can do
- `RestaurantService` returns static restaurant data

## 5. Folder Structure

```text
SwiggyAPI.slnx
01_Introduction_To_WebAPI/
  Project/
    SwiggyAPI.API/
      Controllers/
        RestaurantsController.cs
      Models/
        Restaurant.cs
      Services/
        ServiceCollectionExtensions.cs
      Interfaces/
        IRestaurantService.cs
      Implementations/
        RestaurantService.cs
      Program.cs
  Explanation_Topic/
    Introduction_To_ASPNET_Core_Web_API_Explanation.md
  Interview_QA/
    Introduction_To_ASPNET_Core_Web_API_Interview_QA.md
  Exercises/
    Introduction_To_WebAPI_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Create the Web API project

We created a controller-based ASP.NET Core Web API project using .NET 8.

Why controller-based project?

- easier for beginners to understand
- good for enterprise APIs
- keeps routing and HTTP methods very clear

### Step 2: Add a model

`Restaurant.cs` represents the shape of restaurant data.

This is our API data object for now.

### Step 3: Add an interface

`IRestaurantService` says:

- get all restaurants
- get one restaurant by id

Why interface?

- keeps code clean
- improves testability
- supports loose coupling
- common in real projects

### Step 4: Add service implementation

`RestaurantService` contains static list data.

For learning:

- no database yet
- no repository yet
- no Dapper or Entity Framework yet

This helps us focus only on Web API basics.

### Step 5: Add controller

`RestaurantsController` exposes API endpoints using HTTP GET.

Endpoints:

- `GET /api/restaurants`
- `GET /api/restaurants/{restaurantId}`

### Step 6: Register the service

In `Program.cs`, we register `IRestaurantService` with `RestaurantService`.

This is called Dependency Injection registration.

## 7. Internal Working

When the application starts:

1. `Program.cs` builds the ASP.NET Core app
2. Services are registered in the DI container
3. ASP.NET Core starts listening for HTTP requests
4. When a request comes, routing finds the correct controller action
5. Controller receives required dependencies from DI
6. Controller calls service
7. Service returns data
8. API sends JSON response to the client

## 8. Request Lifecycle

Example request:

```http
GET /api/restaurants/2
```

Flow:

1. Browser or Postman sends request
2. ASP.NET Core routing checks controller routes
3. `RestaurantsController.GetRestaurantById` is matched
4. `restaurantId = 2` is taken from the URL
5. Controller calls `_restaurantService.GetRestaurantByIdAsync(2)`
6. Service searches the static list using LINQ
7. Matching restaurant is returned
8. Controller returns `200 OK` with JSON

If not found:

- controller returns `404 Not Found`

## 9. Code Explanation

### `Program.cs`

- `builder.Services.AddControllers();`
  This enables controller support.

- `builder.Services.AddApplicationServices();`
  This registers our custom services.

- `app.MapControllers();`
  This tells ASP.NET Core to expose controller routes as endpoints.

### `RestaurantsController`

- `[ApiController]`
  Enables automatic API behavior like validation-friendly responses and route binding support.

- `[Route("api/[controller]")]`
  Creates route based on controller name.
  `RestaurantsController` becomes `api/restaurants`.

- `[HttpGet]`
  Handles GET request for all restaurants.

- `[HttpGet("{restaurantId:int}")]`
  Handles GET request for one restaurant by integer id.

### `RestaurantService`

- stores temporary static list
- uses `Task.FromResult(...)` because currently data is in memory
- uses `FirstOrDefault(...)` to find one item

## 10. Common Errors

- forgetting `builder.Services.AddControllers()`
- forgetting `app.MapControllers()`
- route spelling mistakes
- controller name not ending with `Controller`
- service not registered in DI
- namespace mismatch after moving files
- returning entity not found without proper `404`

## 11. Debugging Tips

- check API URL carefully
- verify port from launch output
- put breakpoint inside controller
- if service is null, check DI registration
- if endpoint gives 404, check route attribute and HTTP method
- if build fails, check namespace and missing `using`

## 12. Best Practices

- keep controllers thin
- put business logic in service layer
- use interfaces for service contracts
- use meaningful route names
- return proper HTTP status codes
- start simple before adding database complexity
- keep methods small and easy to read

## 13. Real-Time Company Usage

In real companies, ASP.NET Core Web API is commonly used for:

- e-commerce backend APIs
- banking services
- internal enterprise systems
- mobile application backends
- microservices
- order management systems

Even when the project becomes large, the same basics still apply:

- routing
- controllers
- DI
- services
- request/response lifecycle

## 14. Summary Notes

- ASP.NET Core Web API is used to build backend HTTP services
- Controller receives request
- Service contains business logic
- Interface creates loose coupling
- `Program.cs` configures the application
- API returns JSON to clients
- We started with static data to understand the flow clearly

This is the correct starting point before learning:

- HTTP basics
- routing
- services
- Dependency Injection
- Swagger
- middleware
- exception handling
