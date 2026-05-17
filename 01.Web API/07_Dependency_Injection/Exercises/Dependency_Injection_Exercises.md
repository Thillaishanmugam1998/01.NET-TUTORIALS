# Exercises - Dependency Injection in ASP.NET Core Web API

## Exercise 1

Run:

```http
GET /api/dependency/current
```

Write down:

- service name
- environment name
- message

## Exercise 2

Run:

```http
GET /api/restaurants/message
```

Explain how the message travels from:

- controller
- service
- injected message service

## Exercise 3

Open `ServiceCollectionExtensions.cs` and explain:

- `AddScoped<IRestaurantService, RestaurantService>()`
- `AddScoped<IRequestInfoService, RequestInfoService>()`
- `AddScoped<IRestaurantMessageService, RestaurantMessageService>()`

## Exercise 4

Open `RestaurantsController.cs` and answer:

1. Which service is injected into controller?
2. Where is that service registered?
3. Does the controller create that object manually?

## Exercise 5

Answer this interview-style question:

What will happen if `IRestaurantService` is injected into a controller but not registered in the DI container?
