# Default ASP.NET Core Web API Files and Folders

## 1. Topic Overview

When we create an ASP.NET Core Web API project, Visual Studio or the `dotnet` CLI gives us a default set of files and folders.

These files are not random.

Each file has a clear job.

In our `SwiggyAPI` learning path, understanding these files is very important because this is the base on which we will build:

- restaurant management
- food items
- cart
- orders
- authentication
- middleware
- database integration later

Simple idea:

- some files configure the app
- some files define API endpoints
- some files store models
- some files register services
- some files help us run and test the API

If you understand these default files clearly, the rest of Web API becomes much easier.

## 2. Why It Is Used

We use the default ASP.NET Core Web API structure because it gives us:

- a clean starting point
- standard project organization
- controller support
- environment-based configuration
- easy local testing
- scalability for future enterprise features

Real benefit:

When a new developer joins a team, they can quickly understand the project if files are kept in the standard structure.

## 3. Real-Time Example

Imagine a real Swiggy-like backend.

When frontend calls:

```http
GET /api/restaurants
```

The request touches multiple files:

1. `Program.cs` starts and configures the app
2. `RestaurantsController.cs` receives the request
3. `IRestaurantService.cs` defines what logic is needed
4. `RestaurantService.cs` returns data
5. `Restaurant.cs` defines response shape
6. `appsettings.json` can provide settings used by the app

So one API call is not handled by just one file.

It is handled by a small group of files working together.

## 4. Architecture Flow

We are continuing the same simple architecture:

```text
Client
  |
  v
Program.cs
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
Model
  |
  v
JSON Response
```

For this lesson:

- `Program.cs` configures the application
- `Controllers` folder receives requests
- `Interfaces` folder defines contracts
- `Implementations` folder contains logic
- `Models` folder stores data classes
- `Properties/launchSettings.json` helps local run setup

## 5. Folder Structure

```text
03_Project_Structure_And_Files_Controllers/
  Project/
    SwiggyAPI.Controllers.API/
      Controllers/
        RestaurantsController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
      Interfaces/
        IRestaurantService.cs
      Implementations/
        RestaurantService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.Controllers.API.http
      SwiggyAPI.Controllers.API.csproj
  Explanation_Topic/
    Project_Structure_And_Files_Controllers_Explanation.md
  Interview_QA/
    Project_Structure_And_Files_Controllers_Interview_QA.md
  Exercises/
    Project_Structure_And_Files_Controllers_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Understand `Program.cs`

`Program.cs` is the starting point of the application.

Its main jobs are:

- create the application builder
- register services
- build the app
- configure middleware
- map controllers
- run the application

Important lines:

```csharp
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
app.MapControllers();
```

Meaning:

- enable controller support
- register our custom services
- expose controller endpoints

### Step 2: Understand `Controllers` folder

The `Controllers` folder contains endpoint classes.

In our project:

- `RestaurantsController.cs`

This file directly handles URLs like:

- `GET /api/restaurants`
- `GET /api/restaurants/2`
- `GET /api/restaurants/open`
- `POST /api/restaurants`

### Step 3: Understand `Models` folder

The `Models` folder contains simple C# classes used to send or receive data.

We currently have:

- `Restaurant.cs`
- `RestaurantCreateRequest.cs`
- `RestaurantUpdateRequest.cs`

Why separate request models?

Because create/update input may be different from output model in real projects.

This is a clean enterprise habit.

### Step 4: Understand `Interfaces` folder

The `Interfaces` folder stores contracts.

Example:

```csharp
public interface IRestaurantService
```

This tells us what the service can do, such as:

- get all restaurants
- get restaurant by id
- get open restaurants
- add restaurant
- update restaurant
- delete restaurant

### Step 5: Understand `Implementations` folder

The `Implementations` folder stores actual logic classes.

In our lesson:

- `RestaurantService.cs`

This class uses a static list for now.

That is correct for beginner learning because we are not adding database complexity yet.

### Step 6: Understand `Services` folder

The `Services` folder contains helper classes related to service registration.

We added:

- `ServiceCollectionExtensions.cs`

Why?

Because it keeps `Program.cs` clean.

Instead of writing many registrations in `Program.cs`, we move them into one extension method:

```csharp
builder.Services.AddApplicationServices();
```

### Step 7: Understand `appsettings.json`

This file stores application configuration.

Examples in real projects:

- logging levels
- connection strings
- API keys
- feature flags

In our current lesson, it mainly contains logging settings.

### Step 8: Understand `appsettings.Development.json`

This file stores settings specific to the Development environment.

Real-time usage:

- developers may use different logging
- local machine settings may differ from production

### Step 9: Understand `launchSettings.json`

This file helps run the API locally in Visual Studio or with local profiles.

It defines:

- HTTP URL
- HTTPS URL
- environment name

This is mainly for developer convenience.

### Step 10: Understand `.http` file

`SwiggyAPI.Controllers.API.http` helps test API endpoints quickly.

You can call:

- `GET`
- `POST`
- `PUT`
- `DELETE`

without opening Postman.

This is very useful during learning and development.

### Step 11: Understand `.csproj`

`SwiggyAPI.Controllers.API.csproj` is the project file.

It tells .NET:

- this is a Web SDK project
- target framework is `.NET 8`
- nullable is enabled
- implicit usings are enabled

This file is very important in enterprise projects because package references and build settings live here.

## 7. Full Code

Main project files for this topic:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Interfaces/IRestaurantService.cs`
- `Implementations/RestaurantService.cs`
- `Models/Restaurant.cs`
- `Models/RestaurantCreateRequest.cs`
- `Models/RestaurantUpdateRequest.cs`
- `Services/ServiceCollectionExtensions.cs`
- `appsettings.json`
- `appsettings.Development.json`
- `Properties/launchSettings.json`
- `SwiggyAPI.Controllers.API.http`
- `SwiggyAPI.Controllers.API.csproj`

## 8. Code Explanation

### `Program.cs`

This is the entry point.

Important lines:

- `builder.Services.AddControllers();`
  This enables controller-based Web API support.

- `builder.Services.AddApplicationServices();`
  This registers our custom service classes into Dependency Injection.

- `app.UseHttpsRedirection();`
  This redirects HTTP traffic to HTTPS.

- `app.MapControllers();`
  This exposes controller routes to the outside world.

### `RestaurantsController.cs`

This file receives HTTP requests.

Important attributes:

- `[ApiController]`
- `[Route("api/[controller]")]`
- `[HttpGet]`
- `[HttpPost]`
- `[HttpPut]`
- `[HttpDelete]`

This file should stay thin.

Meaning:

- receive request
- call service
- return response

### `RestaurantService.cs`

This file contains business logic for now.

Example:

- return all restaurants
- filter open restaurants
- add one restaurant
- update one restaurant
- delete one restaurant

### `ServiceCollectionExtensions.cs`

This is used for Dependency Injection registration.

Example:

```csharp
services.AddScoped<IRestaurantService, RestaurantService>();
```

Meaning:

When controller asks for `IRestaurantService`, give `RestaurantService`.

## 9. Request Lifecycle

Example request:

```http
GET /api/restaurants/open
```

Full flow:

1. Browser, Postman, or `.http` file sends request
2. ASP.NET Core app is already started by `Program.cs`
3. `app.MapControllers()` makes controller routes available
4. Routing checks `RestaurantsController`
5. `[HttpGet("open")]` matches the URL
6. Controller calls `_restaurantService.GetOpenRestaurants()`
7. `RestaurantService` filters static list using LINQ
8. Controller returns `200 OK`
9. Client receives JSON array

This is the same request flow used in real companies, even when database and authentication are added later.

## 10. Interview Questions

### Beginner

What is `Program.cs` in ASP.NET Core Web API?

It is the application startup file where we configure services and the request pipeline.

### Intermediate

Why do we keep models in a separate folder?

Because models represent data clearly and keep the project organized and easy to maintain.

### Advanced

Why is `ServiceCollectionExtensions.cs` useful?

It keeps service registration clean, reusable, and scalable as the project grows.

### Real-time scenario

A new developer joins the team and cannot find where `/api/restaurants` is handled. Which files should they check first?

They should check:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Interfaces/IRestaurantService.cs`
- `Implementations/RestaurantService.cs`

## 11. Exercises

1. Open `Program.cs` and explain each line in your own words.
2. Open `launchSettings.json` and identify the HTTP and HTTPS ports.
3. Open `SwiggyAPI.Controllers.API.http` and run `GET /api/restaurants`.
4. Trace the request flow from controller to service.
5. Explain why `RestaurantCreateRequest` and `RestaurantUpdateRequest` are separate files.

## 12. Common Errors

- forgetting `builder.Services.AddControllers()`
- forgetting `app.MapControllers()`
- placing models inside controller file
- writing business logic directly inside controller
- not registering service in DI
- confusing `appsettings.json` with `launchSettings.json`
- thinking `.http` file is required for production

## 13. Debugging Tips

- if endpoint returns `404`, check route attribute and `app.MapControllers()`
- if service is null, check DI registration
- if app is not starting, check `Program.cs` and `.csproj`
- if wrong port is used, check `launchSettings.json`
- if request body is not binding, check request model and JSON shape

## 14. Best Practices

- keep `Program.cs` simple and readable
- keep controllers thin
- keep business logic in services
- keep models in separate files
- use interfaces for loose coupling
- use environment files correctly
- use `.http` file for fast testing during development
- do not add unnecessary layers until needed

## 15. Summary Notes

- default ASP.NET Core Web API files each have a clear purpose
- `Program.cs` starts and configures the app
- `Controllers` handle requests
- `Models` define data shape
- `Interfaces` define contracts
- `Implementations` contain business logic
- `Services` can organize DI registration
- `appsettings.json` stores config
- `launchSettings.json` helps local run setup
- `.http` file helps test endpoints quickly

This lesson is the foundation for all coming lessons in the same `SwiggyAPI` solution.
