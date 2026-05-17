# Exercises - Swagger API in ASP.NET Core Web API

## Exercise 1

Run the project and open:

```text
/swagger
```

Write down:

- how many controllers are visible
- which endpoints are visible

## Exercise 2

From Swagger UI, test:

```http
GET /api/restaurants
```

Answer:

1. What response did you get?
2. Did Swagger show sample response structure?

## Exercise 3

From Swagger UI, test:

```http
GET /api/serviceLifetime/compare
```

Answer:

1. Which values changed after refreshing the request?
2. Which values stayed same?

## Exercise 4

Use Swagger UI to call:

```http
POST /api/restaurants
```

Create a sample restaurant with your own values.

Then call:

```http
GET /api/restaurants
```

Verify whether the new restaurant is added.

## Exercise 5

Explain in simple words:

1. Why do we use `AddSwaggerGen()`?
2. Why do we use `UseSwaggerUI()`?
3. Why is Swagger helpful for frontend teams?

## Exercise 6

Open `Program.cs` and identify:

- Swagger service registration line
- Swagger middleware line
- Controller mapping line

## Exercise 7

Interview-style question:

Your API is running, but Swagger UI is not opening. Name three things you will check first.
