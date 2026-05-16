# Project Structure and Files - Controllers - Interview Q&A

## Beginner Questions

### 1. What is a controller in ASP.NET Core Web API?

A controller is a class that handles incoming HTTP requests and sends back HTTP responses.

Real-time usage:
Frontend apps, mobile apps, and admin panels call controller endpoints to get or save data.

Common mistake:
Thinking controller is the place for all business logic. It should mainly handle request and response flow.

### 2. What is the purpose of the `Controllers` folder?

It organizes all API endpoint classes in one place.

Real-time usage:
Large projects usually have many controllers like `OrdersController`, `UsersController`, and `PaymentsController`.

Common mistake:
Keeping controller files in random folders and making project navigation confusing.

### 3. Why do controllers inherit from `ControllerBase`?

`ControllerBase` provides useful methods such as `Ok()`, `NotFound()`, `BadRequest()`, and `CreatedAtAction()`.

Common mistake:
Using plain classes and then missing helpful API methods.

## Intermediate Questions

### 4. What does `[ApiController]` do?

It marks the class as an API controller and enables API-friendly features.

Real-time usage:
It is standard in modern ASP.NET Core Web API projects.

Common mistake:
Forgetting the attribute and then wondering why API behavior feels inconsistent.

### 5. What does `[Route("api/[controller]")]` mean?

It creates the route using the controller name.

For `RestaurantsController`, the route becomes:

```text
api/restaurants
```

Common mistake:
Not understanding that `[controller]` automatically uses the controller class name without the word `Controller`.

### 6. What is an action method?

An action method is a public method inside a controller that handles a request.

Example:

- `GetRestaurants()`
- `AddRestaurant()`

## Advanced Questions

### 7. Why should controller logic be thin?

Thin controllers are easier to test, maintain, and read.

Real-time usage:
In enterprise projects, business rules are usually kept in services, not controllers.

Common mistake:
Writing validation, SQL logic, and complex rules directly in controller methods.

### 8. Why do we use `ActionResult<T>`?

Because it allows the action to return both data and HTTP status responses.

Example:

- `Ok(data)`
- `NotFound()`

### 9. What is route constraint in `{restaurantId:int}`?

It means the route value must be an integer.

Real-time usage:
This helps routing become clearer and avoids wrong matches.

## Real-Time Scenario Questions

### 10. The API is running but `/api/restaurants` returns 404. What will you check?

Check:

- controller route attribute
- `app.MapControllers()`
- controller class name
- HTTP method
- correct base URL and port

### 11. Why do we use a separate service after controller?

Because controllers should handle communication, and services should handle business logic.

Real-time usage:
This separation makes the project scalable when the system grows.

### 12. Why was `/api/restaurants/open` added as a custom route?

It demonstrates how controllers can expose both standard and custom route patterns.

Common mistake:
Creating unclear routes that do not describe the purpose of the action.

## Quick Revision Points

- controller receives request
- action method handles endpoint
- route attribute maps URL
- HTTP verb attribute maps request type
- controller should stay thin
- service should contain logic
