# Dependency Injection in ASP.NET Core Web API - Interview Q&A

## Beginner Questions

### 1. What is Dependency Injection?

Dependency Injection is a way to give required objects to a class from outside instead of creating them inside the class.

Real-time usage:
Controllers receive services automatically from ASP.NET Core.

Common mistake:
Thinking DI is only a theory topic. It is used in almost every real ASP.NET Core project.

### 2. What is a dependency?

A dependency is an object that another class needs to do its work.

Example:

- `RestaurantsController` depends on `IRestaurantService`

### 3. What is constructor injection?

Constructor injection means dependencies are passed through the constructor.

Real-time usage:
This is the most common DI style in ASP.NET Core.

Common mistake:
Creating service objects manually inside action methods.

## Intermediate Questions

### 4. Where do we register services in ASP.NET Core?

Usually in:

- `Program.cs`
- extension methods like `ServiceCollectionExtensions.cs`

### 5. Why do we use interfaces with Dependency Injection?

Because interfaces create loose coupling and make implementation changes easier.

Real-time usage:
You can replace one implementation with another without changing controller code.

### 6. Can a service depend on another service?

Yes.

Real-time usage:
In this lesson, `RestaurantService` depends on `IRestaurantMessageService`.

## Advanced Questions

### 7. What happens if a dependency is not registered in DI?

ASP.NET Core throws a runtime error when it tries to create the class that needs that dependency.

Common mistake:
Forgetting registration and then debugging the wrong place.

### 8. Why is DI important in enterprise projects?

Because it improves:

- maintainability
- testability
- separation of concerns
- flexibility

### 9. Is Dependency Injection only for controllers?

No.

It is used in:

- controllers
- services
- middleware
- filters
- hosted services

## Real-Time Scenario Questions

### 10. A new service was created but injection is failing. What will you check first?

Check:

- whether interface exists
- whether implementation exists
- whether service registration exists
- whether constructor parameter type is correct

### 11. Why is manually using `new RestaurantService()` inside controller a poor approach?

Because it tightly couples the controller to the implementation and makes testing and maintenance harder.

### 12. How does ASP.NET Core know which implementation to create for an interface?

From the service registration in the DI container.

Example:

```csharp
services.AddScoped<IRestaurantService, RestaurantService>();
```

## Quick Revision Points

- DI gives required objects automatically
- constructor injection is common in ASP.NET Core
- services must be registered
- interfaces help loose coupling
- services can depend on other services too
