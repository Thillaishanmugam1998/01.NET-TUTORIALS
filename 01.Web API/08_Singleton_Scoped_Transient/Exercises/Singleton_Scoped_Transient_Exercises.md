# Exercises - Singleton vs Scoped vs Transient in ASP.NET Core Web API

## Exercise 1

Run:

```http
GET /api/serviceLifetime/compare
```

Write down:

- controller transient ids
- controller scoped ids
- controller singleton ids
- service transient id
- service scoped id
- service singleton id

## Exercise 2

Run the same request again and compare:

1. Which ids changed?
2. Which ids stayed same?
3. Why?

## Exercise 3

Open `ServiceCollectionExtensions.cs` and explain:

- `AddTransient`
- `AddScoped`
- `AddSingleton`

## Exercise 4

Explain in simple words:

1. Why is `Scoped` common in Web API?
2. Why should `Singleton` be used carefully?
3. Why can `Transient` create many objects?

## Exercise 5

Answer this interview-style question:

If a service should have one object per HTTP request in ASP.NET Core Web API, which lifetime should be used?
