# ASP.NET Core Out Of Process Hosting Model

## 1. Topic Overview

ASP.NET Core Out Of Process Hosting Model means the ASP.NET Core application runs in a separate process from IIS.

In simple words:

- IIS receives the request first
- IIS forwards the request to another ASP.NET Core process
- that separate process usually runs on Kestrel
- the ASP.NET Core app handles the request and sends response back

This is called **out of process** because IIS and the ASP.NET Core application do not run inside the same process.

## 2. Why It Is Used

We use out-of-process hosting when:

- the app runs behind IIS but on a separate ASP.NET Core process
- Kestrel handles the ASP.NET Core application
- we want reverse proxy style hosting
- the hosting environment is based on Kestrel behavior

Simple point:

IIS becomes the front door, but the actual ASP.NET Core app runs separately.

## 3. Real-Time Example

Imagine `SwiggyAPI` is deployed on Windows Server.

Frontend sends:

```http
GET /api/restaurants
```

In out-of-process hosting:

1. IIS receives the request
2. IIS forwards it to the ASP.NET Core application running on Kestrel
3. `SwiggyAPI` processes the request
4. response goes back through IIS to the client

This model is also easy to understand if you think of IIS as a reverse proxy in front of Kestrel.

## 4. Architecture Flow

```text
Client
  |
  v
IIS
  |
  v
ASP.NET Core Module
  |
  v
Kestrel (separate ASP.NET Core process)
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

Important idea:

- IIS and ASP.NET Core app are separate processes
- business architecture still stays the same

## 5. Folder Structure

```text
05_ASPNET_Core_OutOfProcess_Hosting_Model/
  Project/
    SwiggyAPI.OutOfProcessHosting.API/
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
      SwiggyAPI.OutOfProcessHosting.API.http
      SwiggyAPI.OutOfProcessHosting.API.csproj
  Explanation_Topic/
    ASPNET_Core_OutOfProcess_Hosting_Model_Explanation.md
  Interview_QA/
    ASPNET_Core_OutOfProcess_Hosting_Model_Interview_QA.md
  Exercises/
    ASPNET_Core_OutOfProcess_Hosting_Model_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Continue the same `SwiggyAPI` layered architecture

We continue using:

- `Controllers`
- `Models`
- `Interfaces`
- `Implementations`
- `Services`

This keeps all lessons consistent.

### Step 2: Keep the restaurant API from the previous lesson

We still keep:

- `GET /api/restaurants`
- `GET /api/restaurants/{id}`
- `GET /api/restaurants/open`
- `POST /api/restaurants`
- `PUT /api/restaurants/{id}`
- `DELETE /api/restaurants/{id}`

This is important because hosting model should not break the business API flow.

### Step 3: Add a simple hosting details endpoint

We added:

```http
GET /api/hosting/current
```

This endpoint helps us explain runtime behavior using:

- process id
- process name
- machine name
- environment name
- server note

### Step 4: Add `HostingInfo` model

This model stores the hosting-related output data.

### Step 5: Add `IHostingInfoService` and `HostingInfoService`

This keeps the same controller -> interface -> implementation flow.

The controller does not directly read process information.

The service does that work.

### Step 6: Register services in DI

In `ServiceCollectionExtensions.cs` we register:

- `IRestaurantService`
- `IHostingInfoService`

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
- `SwiggyAPI.OutOfProcessHosting.API.http`
- `SwiggyAPI.OutOfProcessHosting.API.csproj`

## 8. Code Explanation

### `Program.cs`

This is still the startup file of the ASP.NET Core application.

Important idea for this topic:

When hosted out of process behind IIS:

- IIS is the front server
- Kestrel runs the ASP.NET Core app in a separate process

Our `Program.cs` still configures:

- controllers
- services
- middleware
- endpoints

But it does not manually switch IIS hosting mode by one line of code.

### `HostingController.cs`

This controller exposes:

```http
GET /api/hosting/current
```

This is only a learning endpoint to understand hosting behavior better.

### `HostingInfoService.cs`

This service reads runtime details and returns a note that explains the out-of-process model.

Real-world note:

If the app is truly behind IIS in out-of-process mode, the ASP.NET Core app usually runs as a separate process instead of inside `w3wp`.

## 9. Request Lifecycle

Example request:

```http
GET /api/hosting/current
```

Flow in out-of-process hosting:

1. Client sends request
2. IIS receives the request
3. ASP.NET Core Module forwards it to Kestrel
4. Kestrel passes request into the ASP.NET Core pipeline
5. `Program.cs` pipeline is already active
6. Routing matches `HostingController`
7. Controller calls `IHostingInfoService`
8. Service reads runtime details
9. Controller returns `200 OK`
10. Response goes back through IIS to client

## 10. Interview Questions

### Beginner

What is out-of-process hosting in ASP.NET Core?

It means IIS and the ASP.NET Core application run in separate processes.

### Intermediate

Which server usually runs the ASP.NET Core app in out-of-process hosting?

Kestrel usually runs the ASP.NET Core application.

### Advanced

Why is out-of-process hosting different from in-process hosting?

Because requests are forwarded from IIS to a separate ASP.NET Core process, which adds an extra hop but keeps hosting separated.

### Real-time scenario

If your API is running behind IIS and Kestrel is handling the ASP.NET Core process separately, which hosting model is being used?

Out-of-process hosting.

## 11. Exercises

1. Run `GET /api/hosting/current`.
2. Note the process name you get locally.
3. Explain why Kestrel is important in out-of-process hosting.
4. Explain why the restaurant controller code remains the same.
5. Compare this lesson with the previous in-process hosting lesson.

## 12. Common Errors

- thinking IIS directly runs the ASP.NET Core code in the same process
- confusing Kestrel with IIS
- assuming hosting model changes controller code
- mixing hosting concept with DI or routing concept
- assuming out-of-process means "outside the server"

## 13. Debugging Tips

- inspect `launchSettings.json`
- know which profile you are running
- use `GET /api/hosting/current` to inspect runtime info
- remember local development process name may differ from deployed IIS setup
- if IIS deployment has issues, check both IIS and ASP.NET Core runtime setup

## 14. Best Practices

- understand IIS and Kestrel roles clearly
- keep hosting topics separate from business logic
- keep controllers thin
- use services for runtime detail collection
- learn both in-process and out-of-process models for interviews and real projects
- always confirm actual hosting environment before diagnosing production issues

## 15. Summary Notes

- out-of-process hosting means IIS and ASP.NET Core app run in separate processes
- IIS receives request first
- Kestrel usually handles the ASP.NET Core app
- request goes through an extra forwarding step
- business code structure still stays the same
- hosting model affects infrastructure, not controller-service architecture
