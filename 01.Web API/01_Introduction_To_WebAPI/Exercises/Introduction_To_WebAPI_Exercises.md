# Exercises - Introduction to ASP.NET Core Web API

## Exercise 1

Add one more restaurant to the static list in `RestaurantService`.

Goal:

- understand how API response changes
- practice modifying service data

## Exercise 2

Add a new property called `EstimatedDeliveryTime` to the `Restaurant` model.

Goal:

- understand model changes
- see JSON response update automatically

## Exercise 3

Create one new endpoint:

```http
GET /api/restaurants/open
```

Return only restaurants where `IsOpen = true`.

Hint:

Use LINQ in service layer.

## Exercise 4

Change route from:

```http
/api/restaurants/{restaurantId}
```

to:

```http
/api/restaurants/details/{restaurantId}
```

Goal:

- understand attribute routing
- understand route template customization

## Exercise 5

Explain the request flow in your own words for:

```http
GET /api/restaurants
```

Write:

1. who sends request
2. which controller handles it
3. which service method runs
4. what response is returned

## Mini Challenge

Teach this topic to a junior developer using only these keywords:

- controller
- route
- service
- JSON
- GET
- request
- response

If you can explain it simply, you have understood the basics well.
