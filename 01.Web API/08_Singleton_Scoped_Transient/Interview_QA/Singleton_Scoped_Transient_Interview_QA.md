# Singleton vs Scoped vs Transient in ASP.NET Core Web API - Interview Q&A

## Beginner Questions

### 1. What is a service lifetime in ASP.NET Core?

A service lifetime tells ASP.NET Core how long a service object should live.

Real-time usage:
When services are registered in DI, lifetime controls whether the object is reused or recreated.

Common mistake:
Thinking service lifetime and business logic are the same thing.

### 2. What does `Transient` mean?

`Transient` means a new object is created every time the service is requested.

### 3. What does `Scoped` mean?

`Scoped` means one object is created for one HTTP request.

### 4. What does `Singleton` mean?

`Singleton` means one object is created once and reused for the whole application lifetime.

## Intermediate Questions

### 5. Which lifetime is most common for request-based services?

`Scoped` is most common for request-based services.

Real-time usage:
Many business services in Web API projects are registered as scoped.

### 6. Why can `Transient` create different values in the same request?

Because DI creates a new object every time the service is resolved.

### 7. Why can `Singleton` be risky if used carelessly?

Because the same object is shared across the whole application lifetime.

If it stores wrong state, bugs can affect many requests.

## Advanced Questions

### 8. Why is choosing correct lifetime important?

Because lifetime affects:

- behavior
- memory usage
- correctness
- shared state
- request safety

### 9. Can a scoped service be different between two requests?

Yes.

It stays same only inside one request, not across all requests.

### 10. Can singleton values remain same across many requests?

Yes.

That is the main point of singleton lifetime.

## Real-Time Scenario Questions

### 11. You need one object per HTTP request. Which lifetime should you use?

Use `Scoped`.

### 12. You need a lightweight helper that can be recreated often. Which lifetime can fit?

`Transient` can fit for lightweight stateless helpers.

### 13. You have application-wide shared read-only settings service. Which lifetime often fits?

`Singleton` often fits.

Common mistake:
Using singleton for request or user-specific changing data.

## Quick Revision Points

- `Transient` = new object every time
- `Scoped` = one object per request
- `Singleton` = one object for app lifetime
- choose lifetime based on behavior, not guesswork
