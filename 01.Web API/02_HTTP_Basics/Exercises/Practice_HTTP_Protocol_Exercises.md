# Exercises - Practice HTTP Protocol

## Exercise 1

Call:

```http
GET /api/restaurants
```

Write down:

- request method
- endpoint
- response status code
- response body type

## Exercise 2

Create a new restaurant using:

```http
POST /api/restaurants
```

Sample JSON:

```json
{
  "name": "Madurai Mess",
  "city": "Madurai",
  "cuisine": "South Indian",
  "rating": 4.3,
  "isOpen": true
}
```

## Exercise 3

Update the created restaurant using:

```http
PUT /api/restaurants/4
```

Change:

- city
- rating
- isOpen

## Exercise 4

Delete restaurant id `4` using:

```http
DELETE /api/restaurants/4
```

Then call:

```http
GET /api/restaurants/4
```

Observe the result.

## Exercise 5

Answer in simple words:

1. Why do we use `POST` for create?
2. Why do we use `PUT` for update?
3. Why do we use `DELETE` for remove?
4. Why should `GET` not change data?
