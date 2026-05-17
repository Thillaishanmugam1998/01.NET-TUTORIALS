# Exercises - ASP.NET Core In-Process Hosting Model

## Exercise 1

Run:

```http
GET /api/hosting/current
```

Write down:

- process id
- process name
- environment name
- machine name

## Exercise 2

Explain in simple words:

1. What does in-process hosting mean?
2. Why is it usually faster?
3. Is it a business logic topic or infrastructure topic?

## Exercise 3

Open `Program.cs` and explain:

- `AddControllers()`
- `AddApplicationServices()`
- `MapControllers()`
- `Run()`

## Exercise 4

Run:

```http
GET /api/restaurants
```

Then explain why the restaurant flow remains the same even when the hosting model changes.

## Exercise 5

Answer this interview-style question:

If an ASP.NET Core Web API is hosted on IIS and the app runs inside the IIS worker process, what hosting model is used?
