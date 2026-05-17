# Default ASP.NET Core Web API Files and Folders - Interview Q&A

## Beginner Questions

### 1. What is the purpose of `Program.cs` in ASP.NET Core Web API?

`Program.cs` is the startup file of the application.

It is used to:

- register services
- configure middleware
- map controllers
- run the application

Real-time usage:
Every ASP.NET Core Web API project starts from this file.

Common mistake:
Thinking `Program.cs` only starts the app. It also configures the app.

### 2. What is the purpose of the `Controllers` folder?

The `Controllers` folder stores API endpoint classes.

Real-time usage:
This is where incoming URLs like `/api/restaurants` are handled.

Common mistake:
Writing controller code in random folders and making the project hard to navigate.

### 3. What is the use of the `Models` folder?

The `Models` folder stores C# classes that represent request and response data.

Real-time usage:
These classes define the shape of data used between client and API.

Common mistake:
Using one giant model for every operation.

### 4. What is `appsettings.json` used for?

It stores application configuration.

Examples:

- logging settings
- connection strings later
- API-related settings

Common mistake:
Confusing configuration file with code file.

## Intermediate Questions

### 5. Why do we use an `Interfaces` folder?

We use interfaces to define contracts between controller and service.

Real-time usage:
This supports loose coupling and better maintainability.

Common mistake:
Skipping interfaces in service-based applications and tightly coupling code.

### 6. Why do we use an `Implementations` folder?

It stores the actual class that implements an interface.

Example:

- `IRestaurantService`
- `RestaurantService`

Real-time usage:
This separation makes projects easier to test and extend.

### 7. What is the purpose of `launchSettings.json`?

It stores local run profiles for development.

It can define:

- port numbers
- environment name
- launch behavior

Common mistake:
Thinking this file is production deployment configuration.

### 8. What is the use of the `.http` file?

It is used to test API endpoints quickly from the IDE.

Real-time usage:
Developers often use it during local API testing.

Common mistake:
Thinking `.http` file is required for the application to run.

## Advanced Questions

### 9. Why do we move DI registration into `ServiceCollectionExtensions.cs`?

Because it keeps `Program.cs` clean and organized.

Real-time usage:
As the number of services grows, extension methods help structure registrations better.

Common mistake:
Writing too many registrations directly in `Program.cs` and making it cluttered.

### 10. Why is standard folder structure important in enterprise projects?

Because it improves:

- maintainability
- onboarding
- readability
- teamwork

Real-time usage:
Large teams depend on predictable project structure.

### 11. Why should controllers stay thin?

Controllers should focus on:

- receiving requests
- routing
- calling services
- returning responses

Real-time usage:
Business logic is easier to maintain when it is inside services.

Common mistake:
Writing validation, SQL logic, and complex calculations inside controllers.

## Real-Time Scenario Questions

### 12. A request is returning 404. Which files should you check first?

Check:

- `Program.cs`
- controller route attributes
- `launchSettings.json`
- `.http` request URL

Real-time usage:
These are the first places developers check during endpoint debugging.

### 13. A service is not being injected into controller. What might be wrong?

Possible issue:

- service was not registered in DI

Check:

- `ServiceCollectionExtensions.cs`
- `Program.cs`

Common mistake:
Creating the service class but forgetting registration.

### 14. When should database-related folders be added?

Only when the project reaches the database learning stage.

In this learning path:

- start with static data
- understand API flow first
- add database later step-by-step

This is a better beginner-friendly approach.

## Quick Revision Points

- `Program.cs` configures and starts the app
- `Controllers` handle endpoints
- `Models` store data classes
- `Interfaces` define contracts
- `Implementations` contain logic
- `Services` can hold DI registration helpers
- `appsettings.json` stores config
- `launchSettings.json` helps local development
- `.http` file helps endpoint testing
