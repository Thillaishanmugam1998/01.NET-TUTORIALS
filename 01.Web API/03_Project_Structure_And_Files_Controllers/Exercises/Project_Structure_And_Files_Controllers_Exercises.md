# Exercises - Default ASP.NET Core Web API Files and Folders

## Exercise 1

Open the project and identify the purpose of these files:

- `Program.cs`
- `appsettings.json`
- `appsettings.Development.json`
- `launchSettings.json`
- `SwiggyAPI.Controllers.API.csproj`

Write one simple line for each file.

## Exercise 2

Open `RestaurantsController.cs` and answer:

1. Which route is used for all restaurant endpoints?
2. Which action handles `GET /api/restaurants`?
3. Which action handles `GET /api/restaurants/open`?

## Exercise 3

Open `IRestaurantService.cs` and `RestaurantService.cs`.

Write down:

- which methods are declared in interface
- which methods are implemented in service
- why both files are needed

## Exercise 4

Run this request from `.http` file:

```http
GET /api/restaurants/2
```

Then explain the full request flow from:

- `.http` file
- controller
- service
- response

## Exercise 5

Explain in simple words:

1. Why should controllers be inside `Controllers` folder?
2. Why should models be in `Models` folder?
3. Why should business logic not be written directly inside controller?
4. Why is standard structure important in real-time projects?
