# Dependency Injection in ASP.NET Core Web API

## 1. Topic Overview

Dependency Injection means giving required objects to a class from outside instead of creating them manually inside the class.

In simple words:

- controller needs a service
- ASP.NET Core creates that service object
- ASP.NET Core gives it to the controller automatically

This is called Dependency Injection, often shortened as `DI`.

In our `SwiggyAPI` project:

- `RestaurantsController` needs `IRestaurantService`
- ASP.NET Core gives that object automatically

## 2. Why It Is Used

We use Dependency Injection because it makes code:

- clean
- loosely coupled
- easy to test
- easy to maintain
- easy to extend

Without DI, controller may do this:

```csharp
var service = new RestaurantService();
```

That is not a good real-time approach because:

- controller becomes tightly coupled
- changing implementation becomes harder
- testing becomes difficult

## 3. Real-Time Example

Imagine `SwiggyAPI` has:

- `RestaurantsController`
- `RestaurantService`
- `CouponService`
- `OrderService`
- `PaymentService`

If every controller manually creates every service using `new`, the project becomes messy.

With DI:

- services are registered once
- ASP.NET Core creates them when needed
- controllers receive them automatically

This is the standard enterprise approach.

## 4. Architecture Flow

```text
Client
  |
  v
Controller
  |
  v
IRestaurantService
  |
  v
RestaurantService
  |
  v
IRestaurantMessageService
  |
  v
RestaurantMessageService
  |
  v
Response
```

Also:

```text
Program.cs
  |
  v
DI Container
  |
  v
Creates required service objects automatically
```

## 5. Folder Structure

```text
07_Dependency_Injection/
  Project/
    SwiggyAPI.DependencyInjection.API/
      Controllers/
        RestaurantsController.cs
        DependencyController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
        DependencyInfo.cs
      Interfaces/
        IRestaurantService.cs
        IRequestInfoService.cs
        IRestaurantMessageService.cs
      Implementations/
        RestaurantService.cs
        RequestInfoService.cs
        RestaurantMessageService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.DependencyInjection.API.http
      SwiggyAPI.DependencyInjection.API.csproj
  Explanation_Topic/
    Dependency_Injection_Explanation.md
  Interview_QA/
    Dependency_Injection_Interview_QA.md
  Exercises/
    Dependency_Injection_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Keep the same `SwiggyAPI` architecture

We continue using:

- `Controllers`
- `Models`
- `Interfaces`
- `Implementations`
- `Services`

This keeps continuity with previous lessons.

### Step 2: Register services in one place

In `ServiceCollectionExtensions.cs`, we register services like:

```csharp
services.AddScoped<IRestaurantService, RestaurantService>();
```

Meaning:

- when someone asks for `IRestaurantService`
- ASP.NET Core will create `RestaurantService`

### Step 3: Inject service into controller

In `RestaurantsController`, we use constructor injection:

```csharp
public RestaurantsController(IRestaurantService restaurantService)
```

This means controller is not creating the service.

ASP.NET Core creates and supplies it.

### Step 4: Inject one service inside another service

In this lesson, `RestaurantService` also depends on:

```csharp
IRestaurantMessageService
```

This is important because DI is not only for controllers.

It also works between services.

### Step 5: Add a simple DI demo endpoint

We added:

```http
GET /api/dependency/current
```

This endpoint returns a simple message showing DI is working.

### Step 6: Add one more practical endpoint

We added:

```http
GET /api/restaurants/message
```

This shows:

- controller calls `IRestaurantService`
- `RestaurantService` internally uses `IRestaurantMessageService`

That is real service-to-service DI.

## 7. Full Code

Main code files in this lesson:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Controllers/DependencyController.cs`
- `Interfaces/IRestaurantService.cs`
- `Interfaces/IRequestInfoService.cs`
- `Interfaces/IRestaurantMessageService.cs`
- `Implementations/RestaurantService.cs`
- `Implementations/RequestInfoService.cs`
- `Implementations/RestaurantMessageService.cs`
- `Models/Restaurant.cs`
- `Models/RestaurantCreateRequest.cs`
- `Models/RestaurantUpdateRequest.cs`
- `Models/DependencyInfo.cs`
- `Services/ServiceCollectionExtensions.cs`
- `Properties/launchSettings.json`
- `appsettings.json`
- `appsettings.Development.json`
- `SwiggyAPI.DependencyInjection.API.http`
- `SwiggyAPI.DependencyInjection.API.csproj`

## 8. Code Explanation

### `Program.cs`

This file starts the app and registers framework features.

Important lines:

- `builder.Services.AddControllers();`
- `builder.Services.AddApplicationServices();`

The first line registers controllers.

The second line registers our custom services.

### `ServiceCollectionExtensions.cs`

This file is very important for DI.

Example:

```csharp
services.AddScoped<IRestaurantService, RestaurantService>();
```

Meaning:

- interface = `IRestaurantService`
- implementation = `RestaurantService`

When ASP.NET Core sees `IRestaurantService`, it creates `RestaurantService`.

### `RestaurantsController.cs`

This controller receives `IRestaurantService` in constructor.

That object is not manually created.

ASP.NET Core injects it automatically.

### `RestaurantService.cs`

This service itself depends on another service:

```csharp
IRestaurantMessageService
```

This proves DI works at multiple layers.

### `DependencyController.cs`

This controller shows a simple DI-specific endpoint for learning.

## 9. Request Lifecycle

Example request:

```http
GET /api/restaurants/message
```

Flow:

1. Client sends request
2. ASP.NET Core routing finds `RestaurantsController`
3. ASP.NET Core creates `RestaurantsController`
4. While creating it, ASP.NET Core injects `IRestaurantService`
5. ASP.NET Core creates `RestaurantService`
6. While creating it, ASP.NET Core injects `IRestaurantMessageService`
7. Controller action calls service method
8. Service gets message from injected message service
9. Response is returned to client

This is the core idea of DI.

## 10. Interview Questions

### Beginner

What is Dependency Injection?

Dependency Injection is a design approach where required objects are provided to a class from outside instead of being created manually inside it.

### Intermediate

Why do we use interfaces with DI?

Interfaces create loose coupling between contract and implementation.

### Advanced

Can one service depend on another service in ASP.NET Core DI?

Yes. DI works not only in controllers, but also in services and other framework-managed classes.

### Real-time scenario

If a controller constructor takes `IRestaurantService` but the service is not registered, what will happen?

The application will fail when ASP.NET Core tries to create the controller because DI cannot resolve that dependency.

## 11. Exercises

1. Run `GET /api/dependency/current`.
2. Run `GET /api/restaurants/message`.
3. Explain how `IRestaurantMessageService` reaches `RestaurantService`.
4. Open `ServiceCollectionExtensions.cs` and explain each registration.
5. Comment out one registration, run the app, and observe the DI error.

## 12. Common Errors

- forgetting to register service in DI container
- injecting interface without implementation registration
- manually creating service with `new` inside controller
- putting too much logic inside controller
- confusing service registration with route mapping

## 13. Debugging Tips

- if app fails at runtime, check whether service is registered
- read DI error message carefully
- inspect constructor parameters of controller and service
- verify interface and implementation namespaces
- keep registrations grouped in one extension method

## 14. Best Practices

- inject dependencies through constructor
- depend on interfaces, not concrete classes
- keep controllers thin
- keep service registration organized
- use DI for both controllers and services
- avoid manual object creation inside controller when DI is the better choice

## 15. Summary Notes

- Dependency Injection means required objects are supplied automatically
- ASP.NET Core has built-in DI container
- controllers can receive services using constructor injection
- services can also receive other services
- registrations are usually added in `Program.cs` or extension methods
- DI keeps code clean, testable, and maintainable
