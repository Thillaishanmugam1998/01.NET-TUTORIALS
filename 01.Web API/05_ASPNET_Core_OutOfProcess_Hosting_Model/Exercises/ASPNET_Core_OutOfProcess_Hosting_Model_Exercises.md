# Exercises - ASP.NET Core Out Of Process Hosting Model

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
- web server name

## Exercise 2

Explain in simple words:

1. What does out-of-process hosting mean?
2. What is the role of IIS?
3. What is the role of Kestrel?

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

Then explain why the restaurant API flow remains the same even though the hosting model changes.

## Exercise 5

Compare:

- In-process hosting
- Out-of-process hosting

Write two simple differences.
