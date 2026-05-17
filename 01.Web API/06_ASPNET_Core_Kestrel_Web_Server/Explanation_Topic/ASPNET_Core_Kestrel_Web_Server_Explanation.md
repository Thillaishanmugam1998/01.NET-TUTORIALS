# ASP.NET Core Kestrel Web Server

## 1. Topic Overview

Kestrel is the default web server used by ASP.NET Core applications.

In simple words:

- when our ASP.NET Core API starts
- Kestrel listens for HTTP or HTTPS requests
- Kestrel passes the request into the ASP.NET Core pipeline
- controller and service code handle the request

So Kestrel is the server that actually runs our ASP.NET Core app.

## 2. Why It Is Used

We use Kestrel because it is:

- fast
- lightweight
- cross-platform
- built for ASP.NET Core
- the default server in modern .NET applications

Real point:

Even if IIS or Nginx is in front, Kestrel often still runs the ASP.NET Core app behind them.

## 3. Real-Time Example

Imagine `SwiggyAPI` is started using:

```powershell
dotnet run
```

What happens?

1. Kestrel starts
2. Kestrel listens on configured port
3. client sends `GET /api/restaurants`
4. Kestrel receives the request
5. ASP.NET Core routing finds the controller action
6. response is returned as JSON

This is how many real APIs run in development and production.

## 4. Architecture Flow

```text
Client
  |
  v
Kestrel Web Server
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
JSON Response
```

If IIS is used in front:

```text
Client
  |
  v
IIS
  |
  v
Kestrel
  |
  v
ASP.NET Core App
```

## 5. Folder Structure

```text
06_ASPNET_Core_Kestrel_Web_Server/
  Project/
    SwiggyAPI.Kestrel.API/
      Controllers/
        RestaurantsController.cs
        ServerController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
        ServerInfo.cs
      Interfaces/
        IRestaurantService.cs
        IServerInfoService.cs
      Implementations/
        RestaurantService.cs
        ServerInfoService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.Kestrel.API.http
      SwiggyAPI.Kestrel.API.csproj
  Explanation_Topic/
    ASPNET_Core_Kestrel_Web_Server_Explanation.md
  Interview_QA/
    ASPNET_Core_Kestrel_Web_Server_Interview_QA.md
  Exercises/
    ASPNET_Core_Kestrel_Web_Server_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Continue the same `SwiggyAPI` architecture

We continue with the same simple structure:

- `Controllers`
- `Models`
- `Interfaces`
- `Implementations`
- `Services`

This keeps our learning path consistent.

### Step 2: Keep existing restaurant endpoints

We continue using:

- `GET /api/restaurants`
- `GET /api/restaurants/{id}`
- `GET /api/restaurants/open`
- `POST /api/restaurants`
- `PUT /api/restaurants/{id}`
- `DELETE /api/restaurants/{id}`

Why?

Because Kestrel is a hosting and server topic.

Business API flow should remain the same.

### Step 3: Add a simple server info endpoint

We added:

```http
GET /api/server/current
```

This endpoint helps us see:

- current process name
- current process id
- environment name
- machine name
- server name

### Step 4: Add `ServerInfo` model

This model stores simple server-related data.

### Step 5: Add `IServerInfoService` and `ServerInfoService`

This keeps our architecture clean:

- controller receives request
- interface defines operation
- implementation returns data

### Step 6: Register services in DI

In `ServiceCollectionExtensions.cs` we register:

- `IRestaurantService`
- `IServerInfoService`

## 7. Full Code

Main code files in this lesson:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Controllers/ServerController.cs`
- `Interfaces/IRestaurantService.cs`
- `Interfaces/IServerInfoService.cs`
- `Implementations/RestaurantService.cs`
- `Implementations/ServerInfoService.cs`
- `Models/Restaurant.cs`
- `Models/RestaurantCreateRequest.cs`
- `Models/RestaurantUpdateRequest.cs`
- `Models/ServerInfo.cs`
- `Services/ServiceCollectionExtensions.cs`
- `Properties/launchSettings.json`
- `appsettings.json`
- `appsettings.Development.json`
- `SwiggyAPI.Kestrel.API.http`
- `SwiggyAPI.Kestrel.API.csproj`

## 8. Code Explanation

### `Program.cs`

`Program.cs` creates and starts the application.

When the app runs, Kestrel acts as the web server.

Important lines:

- `builder.Services.AddControllers();`
- `builder.Services.AddApplicationServices();`
- `app.MapControllers();`
- `app.Run();`

`app.Run();` starts the web application on Kestrel.

### `ServerController.cs`

This controller exposes:

```http
GET /api/server/current
```

This endpoint is only for learning the Kestrel topic in a practical way.

### `ServerInfoService.cs`

This service reads runtime process and environment details.

Simple note:

If you run the app locally, the process name may be:

- `SwiggyAPI.Kestrel.API`
- `dotnet`
- `iisexpress`

depending on how you started the app.

## 9. Request Lifecycle

Example request:

```http
GET /api/restaurants
```

Flow:

1. Client sends request
2. Kestrel receives the HTTP request
3. ASP.NET Core pipeline starts from `Program.cs`
4. routing checks controller routes
5. `RestaurantsController.GetRestaurants()` is matched
6. controller calls `IRestaurantService`
7. service returns static list
8. controller returns `200 OK`
9. client receives JSON response

## 10. Interview Questions

### Beginner

What is Kestrel in ASP.NET Core?

Kestrel is the default web server for ASP.NET Core applications.

### Intermediate

Does Kestrel work only on Windows?

No. Kestrel is cross-platform and works on Windows, Linux, and macOS.

### Advanced

Can Kestrel run directly without IIS?

Yes. Kestrel can run the ASP.NET Core application directly.

### Real-time scenario

If your ASP.NET Core API is started with `dotnet run`, which server usually handles the request?

Kestrel usually handles the request.

## 11. Exercises

1. Run `GET /api/server/current`.
2. Write down the process name you get.
3. Explain in simple words what Kestrel does.
4. Run `GET /api/restaurants` and trace the full request flow.
5. Explain the difference between Kestrel and controller.

## 12. Common Errors

- thinking Kestrel is a controller feature
- confusing Kestrel with ASP.NET Core middleware
- assuming Kestrel only works behind IIS
- thinking hosting server changes business layer code
- assuming process name is always the same in every environment

## 13. Debugging Tips

- check `launchSettings.json` for ports
- know whether you are running `Project` profile or `IIS Express`
- use `GET /api/server/current` to inspect runtime details
- if app is not reachable, check whether Kestrel started on expected URL
- inspect terminal output after `dotnet run`

## 14. Best Practices

- understand Kestrel as the main ASP.NET Core web server
- keep server details separate from business logic
- keep controllers thin
- use service layer even for server info demo endpoints
- learn how Kestrel works both alone and behind reverse proxies
- always verify actual runtime environment while debugging

## 15. Summary Notes

- Kestrel is the default web server for ASP.NET Core
- it listens for HTTP and HTTPS requests
- it passes requests to the ASP.NET Core pipeline
- it is fast, lightweight, and cross-platform
- it can run directly or behind IIS, Nginx, or Apache
- business code structure stays the same:
  controller -> interface -> implementation
