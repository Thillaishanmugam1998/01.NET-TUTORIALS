# Singleton vs Scoped vs Transient in ASP.NET Core Web API

## 1. Topic Overview

`Singleton`, `Scoped`, and `Transient` are service lifetime options in ASP.NET Core Dependency Injection.

In simple words:

- `Transient` creates a new object every time it is requested
- `Scoped` creates one object for one HTTP request
- `Singleton` creates one object for the full application lifetime

These are not different kinds of business services.

They are different ways ASP.NET Core manages service object creation.

## 2. Why It Is Used

We use service lifetimes because not every service should live for the same amount of time.

Examples:

- some services should be created again and again
- some services should stay same only for one request
- some services should stay same for the whole application

Choosing the correct lifetime helps with:

- memory usage
- correctness
- request safety
- maintainability
- performance

## 3. Real-Time Example

Imagine `SwiggyAPI` has these services:

- `CouponCalculationService`
- `CurrentUserService`
- `ApplicationSettingsService`

Possible lifetime ideas:

- `CouponCalculationService` can often be `Transient`
- `CurrentUserService` is commonly `Scoped`
- `ApplicationSettingsService` is often `Singleton`

This is how real enterprise projects think about DI registrations.

## 4. Architecture Flow

```text
Client
  |
  v
ServiceLifetimeController
  |
  v
ITransientIdService
IScopedIdService
ISingletonIdService
ILifetimeReportService
  |
  v
Response showing lifetime behavior
```

Also:

```text
Program.cs
  |
  v
DI Container
  |
  v
AddTransient / AddScoped / AddSingleton
```

## 5. Folder Structure

```text
08_Singleton_Scoped_Transient/
  Project/
    SwiggyAPI.ServiceLifetimes.API/
      Controllers/
        RestaurantsController.cs
        ServiceLifetimeController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
        LifetimeReport.cs
        ServiceLifetimeInfo.cs
      Interfaces/
        IRestaurantService.cs
        ITransientIdService.cs
        IScopedIdService.cs
        ISingletonIdService.cs
        ILifetimeReportService.cs
      Implementations/
        RestaurantService.cs
        TransientIdService.cs
        ScopedIdService.cs
        SingletonIdService.cs
        LifetimeReportService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.ServiceLifetimes.API.http
      SwiggyAPI.ServiceLifetimes.API.csproj
  Explanation_Topic/
    Singleton_Scoped_Transient_Explanation.md
  Interview_QA/
    Singleton_Scoped_Transient_Interview_QA.md
  Exercises/
    Singleton_Scoped_Transient_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Continue the same `SwiggyAPI` architecture

We continue with:

- `Controllers`
- `Models`
- `Interfaces`
- `Implementations`
- `Services`

This keeps continuity with previous lessons.

### Step 2: Keep the restaurant API

We keep the same restaurant endpoints because this lesson is about service lifetime behavior, not business feature changes.

### Step 3: Create three lifetime services

We added:

- `ITransientIdService`
- `IScopedIdService`
- `ISingletonIdService`

Each service returns an `InstanceId`.

That id helps us see when ASP.NET Core creates a new object and when it reuses the same object.

### Step 4: Register each service with different lifetime

In `ServiceCollectionExtensions.cs`:

```csharp
services.AddTransient<ITransientIdService, TransientIdService>();
services.AddScoped<IScopedIdService, ScopedIdService>();
services.AddSingleton<ISingletonIdService, SingletonIdService>();
```

This is the main part of the lesson.

### Step 5: Compare them inside controller

`ServiceLifetimeController` injects each service twice.

Why?

Because:

- two transient injections usually give different ids
- two scoped injections give same id in one request
- two singleton injections give same id always

### Step 6: Compare them inside another service too

We added `ILifetimeReportService` and `LifetimeReportService`.

This service also receives transient, scoped, and singleton services.

So now we can compare:

- ids inside controller
- ids inside another service

That makes the concept clearer.

## 7. Full Code

Main code files in this lesson:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Controllers/ServiceLifetimeController.cs`
- `Interfaces/IRestaurantService.cs`
- `Interfaces/ITransientIdService.cs`
- `Interfaces/IScopedIdService.cs`
- `Interfaces/ISingletonIdService.cs`
- `Interfaces/ILifetimeReportService.cs`
- `Implementations/RestaurantService.cs`
- `Implementations/TransientIdService.cs`
- `Implementations/ScopedIdService.cs`
- `Implementations/SingletonIdService.cs`
- `Implementations/LifetimeReportService.cs`
- `Models/Restaurant.cs`
- `Models/RestaurantCreateRequest.cs`
- `Models/RestaurantUpdateRequest.cs`
- `Models/LifetimeReport.cs`
- `Models/ServiceLifetimeInfo.cs`
- `Services/ServiceCollectionExtensions.cs`
- `Properties/launchSettings.json`
- `appsettings.json`
- `appsettings.Development.json`
- `SwiggyAPI.ServiceLifetimes.API.http`
- `SwiggyAPI.ServiceLifetimes.API.csproj`

## 8. Code Explanation

### `AddTransient`

```csharp
services.AddTransient<ITransientIdService, TransientIdService>();
```

Meaning:

- every time this service is requested
- create a new object

### `AddScoped`

```csharp
services.AddScoped<IScopedIdService, ScopedIdService>();
```

Meaning:

- create one object per HTTP request
- reuse same object inside that request

### `AddSingleton`

```csharp
services.AddSingleton<ISingletonIdService, SingletonIdService>();
```

Meaning:

- create one object once
- reuse it for the whole application lifetime

### `ServiceLifetimeController.cs`

This controller is the main learning controller for this topic.

It exposes:

```http
GET /api/serviceLifetime/compare
```

The response helps us observe lifetime behavior.

### `LifetimeReportService.cs`

This service shows that service lifetimes apply not only in controllers, but also in other services.

## 9. Request Lifecycle

Example request:

```http
GET /api/serviceLifetime/compare
```

Flow:

1. Client sends request
2. ASP.NET Core creates `ServiceLifetimeController`
3. DI resolves transient services
4. DI resolves scoped services
5. DI resolves singleton services
6. DI resolves `ILifetimeReportService`
7. Controller calls `_lifetimeReportService.GetLifetimeReport()`
8. Controller returns ids in response
9. If you call the endpoint again:
   transient ids change
   scoped ids change for new request
   singleton ids stay same

## 10. Interview Questions

### Beginner

What is the difference between `Transient`, `Scoped`, and `Singleton`?

`Transient` creates new object every time.
`Scoped` creates one object per request.
`Singleton` creates one object for full application lifetime.

### Intermediate

Which lifetime is commonly used for request-related services?

`Scoped` is commonly used for request-related services.

### Advanced

What is the risk of using `Singleton` carelessly?

A singleton lives for the whole application lifetime, so if it stores request-specific or changing state incorrectly, it can cause bugs.

### Real-time scenario

If you want one service object per HTTP request in ASP.NET Core Web API, which lifetime should you choose?

`Scoped`.

## 11. Exercises

1. Run `GET /api/serviceLifetime/compare`.
2. Run it again.
3. Compare transient ids, scoped ids, and singleton ids.
4. Explain why scoped ids change between requests but remain same inside one request.
5. Open `ServiceCollectionExtensions.cs` and explain each lifetime registration.

## 12. Common Errors

- using singleton for request-specific data
- not understanding that scoped is per request
- assuming transient means one object per controller
- choosing lifetime randomly without purpose
- forgetting that lifetimes affect behavior and bugs

## 13. Debugging Tips

- add simple ids like this lesson to understand service creation behavior
- compare values across multiple requests
- inspect DI registration carefully
- if behavior feels shared unexpectedly, check for singleton usage
- if object is recreated too often, check for transient usage

## 14. Best Practices

- use `Scoped` for most request-level business services
- use `Transient` for lightweight stateless helper services
- use `Singleton` carefully for app-wide shared services
- avoid storing request-specific state in singleton
- choose lifetime intentionally, not randomly

## 15. Summary Notes

- `Transient` = new object every time
- `Scoped` = one object per HTTP request
- `Singleton` = one object for full app lifetime
- lifetimes are configured in DI registration
- correct lifetime selection is an important real-time backend skill
