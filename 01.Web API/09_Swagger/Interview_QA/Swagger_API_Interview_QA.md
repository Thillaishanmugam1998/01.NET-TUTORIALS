# Swagger API in ASP.NET Core Web API - Interview Q&A

## Beginner Questions

### 1. What is Swagger?

Swagger is a tool used to document and test Web APIs.

Real-time usage:
Developers use Swagger UI to see all API endpoints in one screen and test them quickly.

Common mistake:
Thinking Swagger is only for documentation and not useful for testing.

### 2. What is Swagger UI?

Swagger UI is the browser page that shows API endpoints and allows us to call them directly.

### 3. What is the main benefit of Swagger in Web API?

It makes API understanding and testing easier.

### 4. Which URL usually opens Swagger UI?

Usually:

```text
/swagger
```

## Intermediate Questions

### 5. Why do we use `AddSwaggerGen()`?

We use `AddSwaggerGen()` to register Swagger generation services in ASP.NET Core.

Real-time usage:
Without it, Swagger document generation will not happen.

Common mistake:
Calling `UseSwaggerUI()` but forgetting `AddSwaggerGen()`.

### 6. Why do we use `AddEndpointsApiExplorer()`?

It helps ASP.NET Core discover endpoint metadata that Swagger uses.

### 7. What does `UseSwagger()` do?

It generates the Swagger JSON document at runtime.

### 8. What does `UseSwaggerUI()` do?

It shows the interactive Swagger page in the browser.

## Advanced Questions

### 9. Why is Swagger commonly enabled only in development?

Because many companies want internal API documentation tools mainly in development or controlled environments.

Real-time usage:
Production APIs may keep Swagger restricted, authenticated, or disabled depending on company policy.

Common mistake:
Leaving every internal API documentation page openly available in production without thinking about security.

### 10. Is Swagger a replacement for Postman?

No.

Swagger helps a lot with quick API exploration and testing, but teams may still use Postman for collections, environments, and advanced testing workflows.

### 11. Does Swagger create the API business logic?

No.

Swagger only documents and helps test the API. The real business logic still lives in controllers and services.

## Real-Time Scenario Questions

### 12. A frontend developer asks, "What JSON body should I send to create a restaurant?" How can Swagger help?

Swagger shows the request model and lets the developer test the endpoint directly from the UI.

### 13. A new backend developer joined the team. How does Swagger help onboarding?

Swagger lets the developer see:

- available endpoints
- request body structure
- response format
- route names

This reduces onboarding time.

### 14. Your API works locally but `/swagger` is not opening. What would you check first?

Check:

- whether Swagger services are registered
- whether Swagger middleware is enabled
- whether app is running in `Development`
- whether the package is installed correctly

## Quick Revision Points

- Swagger documents APIs
- Swagger UI helps test APIs in browser
- `AddEndpointsApiExplorer()` collects endpoint metadata
- `AddSwaggerGen()` registers Swagger generation
- `UseSwagger()` creates JSON
- `UseSwaggerUI()` shows the UI
