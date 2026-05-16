# Introduction to ASP.NET Core Web API - Interview Q&A

## Beginner Questions

### 1. What is ASP.NET Core Web API?

ASP.NET Core Web API is a framework used to build REST-based backend services using C#.

Real-time usage:
Companies use it to build APIs for web apps, mobile apps, admin portals, and third-party integrations.

Common mistake:
Many beginners think Web API is only for websites. Actually, it is mainly for backend communication using HTTP.

### 2. What is the difference between Web API and MVC?

MVC is usually used for rendering views like HTML pages.
Web API is mainly used for sending data like JSON.

Real-time usage:
Frontend applications like React, Angular, or mobile apps usually consume Web APIs.

Common mistake:
Thinking both are always the same. They share concepts, but their usage is different.

### 3. What is a controller?

A controller is a class that receives HTTP requests and returns HTTP responses.

Real-time usage:
Each module often has its own controller, like `RestaurantsController` or `OrdersController`.

Common mistake:
Writing all business logic directly inside the controller.

### 4. Why do we use interfaces?

Interfaces define a contract. They tell what methods a service should have without telling how they are implemented.

Real-time usage:
Interfaces make testing easier and allow code to be changed without affecting callers.

Common mistake:
Creating interfaces but not using them through Dependency Injection.

### 5. What is Dependency Injection?

Dependency Injection means ASP.NET Core creates required objects and supplies them automatically.

Real-time usage:
Used everywhere in enterprise projects for services, logging, repositories, HTTP clients, and more.

Common mistake:
Forgetting to register service mappings in `Program.cs`.

## Intermediate Questions

### 6. What is `[ApiController]`?

It is an attribute that gives special API behavior like better parameter binding and automatic validation-friendly behavior.

Real-time usage:
Almost all modern ASP.NET Core APIs use it on controllers.

Common mistake:
Removing it without understanding its benefits.

### 7. What does `[Route("api/[controller]")]` mean?

It defines the URL pattern for the controller.
`[controller]` is replaced with the controller name without the word `Controller`.

Example:
`RestaurantsController` becomes `api/restaurants`.

Common mistake:
Using wrong route patterns and then getting 404 errors.

### 8. Why do we use async and Task in API methods?

Async methods help APIs scale better by not blocking threads while waiting for I/O operations.

Real-time usage:
Very important when calling databases, external APIs, files, queues, or cloud services.

Common mistake:
Using `.Result` or `.Wait()` and causing blocking issues.

### 9. Why do we return `ActionResult<T>`?

It allows returning both data and HTTP status codes.

Example:

- `200 OK` with restaurant data
- `404 Not Found` if restaurant does not exist

Real-time usage:
This is common in production APIs because not every request succeeds.

Common mistake:
Returning only plain objects and ignoring proper status handling.

## Advanced Questions

### 10. Why is thin controller and service-based architecture preferred?

Thin controllers keep request handling simple, while services contain business logic. This improves readability, maintainability, and testing.

Real-time usage:
This is a common enterprise approach before introducing more layers like repositories or domain services.

Common mistake:
Putting SQL queries, validations, loops, and business rules directly in controller actions.

### 11. What happens internally when an HTTP request reaches ASP.NET Core Web API?

High-level flow:

1. Kestrel receives the request
2. Middleware pipeline processes the request
3. Routing finds matching endpoint
4. Controller instance is created
5. Dependencies are injected
6. Action method executes
7. Response is serialized to JSON
8. HTTP response is returned

Real-time usage:
Knowing this helps in debugging routing, middleware, authorization, and performance issues.

Common mistake:
Not understanding whether the issue is in routing, service registration, or serialization.

### 12. Why do real projects start with layered architecture even for simple modules?

Because projects grow quickly. A small API today can become a large system tomorrow. Layered structure gives a clean place for each responsibility.

Real-time usage:
This reduces chaos when new developers join and features expand.

Common mistake:
Starting with everything in one file and struggling later during maintenance.

## Real-Time Scenario Questions

### 13. If `GET /api/restaurants/100` returns 500 instead of 404, what will you check?

Check:

- controller logic
- null handling
- service return value
- exception logs
- route mapping

Expected fix:
If restaurant is null, return `NotFound()`.

Common mistake:
Trying to use object properties before checking for null.

### 14. A developer says, "Why do we need a service layer? Controller can do everything." How will you answer?

Simple answer:
Controller can do everything for a tiny demo, but real projects become hard to manage. Service layer separates business logic from request handling.

Real-time usage:
This makes testing, reuse, and future database integration easier.

### 15. In a real Swiggy-like API, where would restaurant filtering logic go?

It should go in the service layer, not inside the controller.

Reason:
Controller should only receive request, call service, and return response.

Common mistake:
Writing large filtering logic directly inside controller actions.

## Quick Revision Points

- Web API builds backend HTTP endpoints
- Controller handles request
- Service handles business logic
- Interface defines contract
- DI creates dependencies
- Routing maps URL to controller action
- `ActionResult<T>` helps return correct HTTP responses
