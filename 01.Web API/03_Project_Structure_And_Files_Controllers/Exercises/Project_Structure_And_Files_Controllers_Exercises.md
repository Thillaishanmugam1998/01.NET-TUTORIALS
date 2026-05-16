# Exercises - Project Structure and Files - Controllers

## Exercise 1

Open `RestaurantsController.cs` and identify:

- base route
- `GET` action
- `POST` action
- custom route action

## Exercise 2

Call:

```http
GET /api/restaurants/open
```

Write down:

- which action method is called
- which service method is called
- what status code is returned

## Exercise 3

Create one new controller action:

```http
GET /api/restaurants/city/chennai
```

Goal:

- read city from route
- filter matching restaurants
- return the result

## Exercise 4

Explain in simple words:

1. Why do we use `ControllerBase`?
2. Why do we use `[Route]`?
3. Why do we use `[HttpGet]`?
4. Why should controller not directly contain all logic?

## Exercise 5

Debug the application and place breakpoints in:

- `RestaurantsController`
- `RestaurantService`

Observe the call flow for:

```http
GET /api/restaurants/1
```
