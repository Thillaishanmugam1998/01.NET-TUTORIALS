# Exercises - ASP.NET Core Kestrel Web Server

## Exercise 1

Run:

```http
GET /api/server/current
```

Write down:

- server name
- process id
- process name
- environment name
- machine name

## Exercise 2

Explain in simple words:

1. What is Kestrel?
2. What does Kestrel do?
3. Is Kestrel a controller or a web server?

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

Then explain where Kestrel participates in the request lifecycle.

## Exercise 5

Answer this interview-style question:

If an ASP.NET Core application starts with `dotnet run`, which web server usually handles the request by default?
