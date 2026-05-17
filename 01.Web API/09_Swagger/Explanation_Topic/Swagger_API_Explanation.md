# Swagger API in ASP.NET Core Web API

## 1. Topic Overview

Swagger is a tool that helps us see, understand, and test our Web API endpoints in the browser.

In ASP.NET Core, Swagger usually gives us:

- API documentation
- endpoint list
- request and response structure
- a test UI in browser

In simple words:

Swagger is like a live menu card for our API.

## 2. Why It Is Used

We use Swagger because it makes API learning and testing very easy.

Without Swagger:

- we must remember endpoint URLs manually
- we must use Postman or `.http` files for every test
- new developers need more time to understand the API

With Swagger:

- we can see all endpoints in one place
- we can test `GET`, `POST`, `PUT`, and `DELETE`
- frontend and backend teams understand contracts faster

## 3. Real-Time Example

In `SwiggyAPI`, imagine a frontend developer wants to integrate restaurant APIs.

They want to know:

- what URL to call
- which HTTP method to use
- what request body to send
- what response shape will come back

Swagger helps them quickly open `/swagger` and test:

- `GET /api/restaurants`
- `GET /api/restaurants/1`
- `POST /api/restaurants`
- `PUT /api/restaurants/1`
- `DELETE /api/restaurants/1`

This is very common in real companies during backend and frontend integration.

## 4. Architecture Flow

```text
Browser
  |
  v
Swagger UI
  |
  v
Swagger JSON Document
  |
  v
ASP.NET Core Endpoint Metadata
  |
  v
Controllers
  |
  v
Services
  |
  v
Response
```

Current lesson flow:

```text
Program.cs
  |
  +--> AddControllers()
  +--> AddEndpointsApiExplorer()
  +--> AddSwaggerGen()
  |
  v
app.Build()
  |
  +--> UseSwagger()
  +--> UseSwaggerUI()
  +--> MapControllers()
```

## 5. Folder Structure

```text
09_Swagger/
  Project/
    SwiggyAPI.Swagger.API/
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
      SwiggyAPI.Swagger.API.http
      SwiggyAPI.Swagger.API.csproj
  Explanation_Topic/
    Swagger_API_Explanation.md
  Interview_QA/
    Swagger_API_Interview_QA.md
  Exercises/
    Swagger_API_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Continue the same project

We did not create an unrelated demo.

We continued the same `SwiggyAPI` learning flow and copied the previous lesson structure forward.

### Step 2: Install Swagger package

In the project file:

```xml
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
```

Why?

Because this package provides Swagger generation and Swagger UI support in ASP.NET Core Web API.

### Step 3: Register Swagger services

In `Program.cs`:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

Meaning:

- `AddEndpointsApiExplorer()` helps ASP.NET Core read endpoint information
- `AddSwaggerGen()` creates Swagger document generation support

### Step 4: Enable Swagger middleware

In `Program.cs`:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

Meaning:

- `UseSwagger()` creates the Swagger JSON document
- `UseSwaggerUI()` shows the browser UI

Why inside development?

Because in many real projects, Swagger UI is enabled mainly in development for safety and simplicity.

### Step 5: Auto-open Swagger page

In `launchSettings.json`:

```json
"launchBrowser": true,
"launchUrl": "swagger"
```

This helps the API open directly to Swagger UI when we run the project locally.

## 7. Full Code

### `Program.cs`

```csharp
using SwiggyAPI.Swagger.API.Services;

var builder = WebApplication.CreateBuilder(args);
// Creates the application builder that prepares services, configuration, logging, and middleware.

// Add services to the container.
builder.Services.AddControllers();
// Registers controller support so ASP.NET Core can route HTTP requests to controller actions.

builder.Services.AddEndpointsApiExplorer();
// Helps ASP.NET Core discover API endpoint details for Swagger document generation.

builder.Services.AddSwaggerGen();
// Registers Swagger generator so we can create interactive API documentation.

builder.Services.AddApplicationServices();
// Registers our custom services and service lifetimes in Dependency Injection container.

var app = builder.Build();
// Builds the final ASP.NET Core application.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Generates the Swagger JSON document at runtime.

    app.UseSwaggerUI();
    // Provides Swagger UI so we can test API endpoints in the browser.
}

app.UseHttpsRedirection();
// Redirects HTTP requests to HTTPS.

app.UseAuthorization();
// Adds authorization middleware to the request pipeline.

app.MapControllers();
// Makes controller routes available as API endpoints.

app.Run();
// Starts the application.
```

### `SwiggyAPI.Swagger.API.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>

</Project>
```

## 8. Code Explanation

### `AddEndpointsApiExplorer()`

This tells ASP.NET Core:

"Read my endpoints and collect API metadata."

Swagger uses that metadata later.

### `AddSwaggerGen()`

This tells ASP.NET Core:

"Generate a Swagger document for my API."

That document is usually available in JSON format.

### `UseSwagger()`

This middleware creates the Swagger JSON endpoint at runtime.

Example:

```text
/swagger/v1/swagger.json
```

### `UseSwaggerUI()`

This middleware gives us the browser page:

```text
/swagger
```

That page is the interactive UI.

## 9. Request Lifecycle

When we open Swagger in browser:

1. Browser requests `/swagger`
2. ASP.NET Core serves Swagger UI
3. Swagger UI requests `/swagger/v1/swagger.json`
4. ASP.NET Core generates API metadata
5. Swagger UI reads controller endpoints
6. UI shows all API actions on screen
7. User clicks `Try it out`
8. Swagger sends real HTTP request to the API endpoint
9. API controller runs service logic
10. Response is shown back in Swagger UI

## 10. Interview Questions

### Beginner

What is Swagger in Web API?

Swagger is a tool used to document and test APIs.

### Intermediate

Why do we use `AddSwaggerGen()`?

We use it to register Swagger document generation services.

### Advanced

Why do many teams enable Swagger UI only in development?

Because production environments often restrict public API documentation UI for safety and operational control.

### Real-time scenario

A frontend team wants to test backend endpoints quickly without asking backend developers for every payload format. What can help?

Swagger UI can help because it shows endpoints, request models, and responses in one place.

## 11. Exercises

1. Run the project and open `/swagger`.
2. Test `GET /api/restaurants`.
3. Test `GET /api/serviceLifetime/compare`.
4. Use Swagger UI to call `POST /api/restaurants`.
5. Explain what `AddSwaggerGen()` and `UseSwaggerUI()` do.

## 12. Common Errors

- forgetting to install Swagger package
- adding `UseSwagger()` without `AddSwaggerGen()`
- expecting Swagger UI to work when it is enabled only in development but app is running in another environment
- wrong route assumption like opening `/swagger/index.html` when app is not configured properly
- thinking Swagger is a replacement for controller logic

## 13. Debugging Tips

- confirm project starts in `Development` environment
- check `/swagger` in browser
- check `/swagger/v1/swagger.json`
- make sure controllers are mapped with `app.MapControllers()`
- make sure the package restore completed successfully

## 14. Best Practices

- enable Swagger early in development
- keep endpoint names clean and meaningful
- use proper request and response models
- in real projects, protect or limit Swagger access in production
- keep Swagger documentation aligned with real API behavior

## 15. Summary Notes

- Swagger helps document and test APIs
- `AddEndpointsApiExplorer()` collects endpoint metadata
- `AddSwaggerGen()` registers Swagger generation
- `UseSwagger()` creates the JSON document
- `UseSwaggerUI()` shows the browser-based testing screen
- Swagger is heavily used in real backend development for collaboration and faster testing
