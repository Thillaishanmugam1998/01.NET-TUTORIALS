# ASP.NET Core In-Process Hosting Model

## 1. Topic Overview

ASP.NET Core In-Process Hosting Model means the ASP.NET Core application runs inside the IIS worker process.

In simple words:

- IIS receives the request
- the ASP.NET Core app runs inside the same IIS process
- the request is handled without going to a separate external process

This is called **in-process** because both IIS and the ASP.NET Core application run in the same process boundary.

For our `SwiggyAPI` project, this topic is important because real company APIs are not just about controllers and services.

They also need a hosting model in production.

## 2. Why It Is Used

We use the in-process hosting model because it gives:

- better performance
- lower request overhead
- simpler IIS integration
- good production support on Windows servers

Why?

Because the request does not need to jump from IIS to another separate ASP.NET Core process.

That reduces extra communication cost.

## 3. Real-Time Example

Imagine `SwiggyAPI` is deployed in a company data center on a Windows Server with IIS.

Frontend app sends:

```http
GET /api/restaurants
```

In in-process hosting:

1. IIS receives the request
2. IIS loads the ASP.NET Core application in the IIS worker process
3. ASP.NET Core middleware and controller handle the request
4. JSON response is returned

This is very common in enterprise Windows hosting environments.

## 4. Architecture Flow

For normal request flow inside our app:

```text
Client
  |
  v
IIS
  |
  v
ASP.NET Core App (same IIS worker process)
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

Important note:

- in-process hosting is mainly related to IIS hosting on Windows
- this is different from out-of-process hosting

## 5. Folder Structure

```text
04_ASPNET_Core_InProcess_Hosting_Model/
  Project/
    SwiggyAPI.Hosting.API/
      Controllers/
        RestaurantsController.cs
        HostingController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
        HostingInfo.cs
      Interfaces/
        IRestaurantService.cs
        IHostingInfoService.cs
      Implementations/
        RestaurantService.cs
        HostingInfoService.cs
      Services/
        ServiceCollectionExtensions.cs
      Properties/
        launchSettings.json
      Program.cs
      appsettings.json
      appsettings.Development.json
      SwiggyAPI.Hosting.API.http
      SwiggyAPI.Hosting.API.csproj
  Explanation_Topic/
    ASPNET_Core_InProcess_Hosting_Model_Explanation.md
  Interview_QA/
    ASPNET_Core_InProcess_Hosting_Model_Interview_QA.md
  Exercises/
    ASPNET_Core_InProcess_Hosting_Model_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Continue the same `SwiggyAPI` architecture

We continue using:

- `Controllers`
- `Models`
- `Interfaces`
- `Implementations`
- `Services`

This keeps the learning path clean and continuous.

### Step 2: Keep restaurant endpoints from previous lesson

We kept:

- `GET /api/restaurants`
- `GET /api/restaurants/{id}`
- `GET /api/restaurants/open`
- `POST /api/restaurants`
- `PUT /api/restaurants/{id}`
- `DELETE /api/restaurants/{id}`

Why?

Because hosting model is an infrastructure topic.

Our business feature should continue without breaking.

### Step 3: Add a simple hosting demo endpoint

We added:

```http
GET /api/hosting/current
```

This helps us inspect:

- current process name
- current process id
- environment name
- machine name

This makes the hosting topic more practical.

### Step 4: Add `HostingInfo` model

This model stores runtime hosting-related data.

### Step 5: Add `IHostingInfoService` and `HostingInfoService`

This follows the same architecture.

Controller does not directly read process details.

Instead:

- controller calls interface
- interface points to implementation
- implementation returns hosting details

### Step 6: Register services in DI

In `ServiceCollectionExtensions.cs` we registered:

- `IRestaurantService`
- `IHostingInfoService`

This is important because controllers depend on services.

## 7. Full Code

Main code files in this lesson:

- `Program.cs`
- `Controllers/RestaurantsController.cs`
- `Controllers/HostingController.cs`
- `Interfaces/IRestaurantService.cs`
- `Interfaces/IHostingInfoService.cs`
- `Implementations/RestaurantService.cs`
- `Implementations/HostingInfoService.cs`
- `Models/Restaurant.cs`
- `Models/RestaurantCreateRequest.cs`
- `Models/RestaurantUpdateRequest.cs`
- `Models/HostingInfo.cs`
- `Services/ServiceCollectionExtensions.cs`
- `Properties/launchSettings.json`
- `appsettings.json`
- `appsettings.Development.json`
- `SwiggyAPI.Hosting.API.http`
- `SwiggyAPI.Hosting.API.csproj`

## 8. Code Explanation

### `Program.cs`

This is where ASP.NET Core application starts.

Important idea for this topic:

When deployed to IIS using the in-process hosting model:

- IIS and ASP.NET Core app run in the same worker process

Our code in `Program.cs` does not manually turn on in-process mode.

That hosting behavior is decided during IIS hosting setup.

But `Program.cs` is still the internal app startup point.

### `HostingController.cs`

This controller exposes:

```http
GET /api/hosting/current
```

It returns simple runtime information for learning.

### `HostingInfoService.cs`

This service reads:

- current process name
- process id
- environment name
- machine name

This helps us understand where the app is running.

Important real-world note:

If the app is hosted in-process inside IIS, the process is usually:

- `w3wp` in IIS
- `iisexpress` in IIS Express

If the app is run directly using `dotnet run`, you may see a different process name.

## 9. Request Lifecycle

Example request:

```http
GET /api/hosting/current
```

When hosted in-process in IIS, the flow is:

1. Client sends request
2. IIS receives the request
3. ASP.NET Core Module forwards request internally
4. ASP.NET Core app runs inside the same IIS worker process
5. `Program.cs` pipeline is already active
6. Routing matches `HostingController`
7. Controller calls `IHostingInfoService`
8. `HostingInfoService` reads runtime details
9. Controller returns `200 OK`
10. Client receives JSON response

## 10. Interview Questions

### Beginner

What is in-process hosting in ASP.NET Core?

It means the ASP.NET Core app runs inside the IIS worker process.

### Intermediate

What is the main benefit of in-process hosting?

Better performance because request handling stays inside the same process.

### Advanced

Does `Program.cs` decide in-process hosting mode?

No. `Program.cs` configures the application. In-process hosting is mainly decided by IIS hosting configuration and deployment setup.

### Real-time scenario

If a company hosts `SwiggyAPI` on Windows Server with IIS and wants lower request overhead, which hosting model is commonly preferred?

In-process hosting is commonly preferred.

## 11. Exercises

1. Run `GET /api/hosting/current`.
2. Write down the process name you see.
3. Explain why process name can differ between IIS Express and `dotnet run`.
4. Run `GET /api/restaurants` and observe that hosting model does not change controller-service architecture.
5. Explain in your own words why hosting model is an infrastructure concept, not a business logic feature.

## 12. Common Errors

- thinking in-process hosting means "inside controller"
- thinking `Program.cs` alone controls IIS hosting mode
- confusing in-process hosting with middleware pipeline
- assuming in-process hosting is for Linux hosting
- mixing hosting concept with database architecture

## 13. Debugging Tips

- check which launch profile is active
- compare `IIS Express` profile and `Project` profile
- inspect `launchSettings.json`
- run `GET /api/hosting/current` to view runtime details
- if deployed to IIS, verify IIS hosting settings and installed .NET runtime

## 14. Best Practices

- keep hosting knowledge separate from business logic
- keep controllers thin even for infrastructure endpoints
- use a service to read runtime details
- understand the difference between local development hosting and production hosting
- use in-process hosting on IIS when Windows hosting performance is important
- always know whether you are running through IIS, IIS Express, or direct Kestrel profile

## 15. Summary Notes

- ASP.NET Core In-Process Hosting Model means app runs inside IIS worker process
- it improves performance by reducing extra process communication
- it is mainly used with IIS on Windows
- `Program.cs` still configures the app startup and pipeline
- business flow remains the same:
  controller -> interface -> implementation
- hosting model changes infrastructure behavior, not business architecture
