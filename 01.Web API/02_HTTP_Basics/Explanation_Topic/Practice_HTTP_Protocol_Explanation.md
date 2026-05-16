# Practice HTTP Protocol

## 1. Topic Overview

HTTP stands for HyperText Transfer Protocol.

It is the communication rule used between:

- browser and server
- mobile app and server
- frontend and backend
- Postman and Web API

In `SwiggyAPI`, HTTP is how the client talks to our restaurant backend.

Simple idea:

- client sends request
- server receives request
- server processes it
- server sends response

## 2. Why It Is Used

Without HTTP, client applications cannot talk to backend APIs in a standard way.

We use HTTP to:

- read data
- create data
- update data
- delete data
- send headers
- send status codes
- structure communication clearly

## 3. Real-Time Example

Swiggy mobile app needs to show restaurants.

It sends:

```http
GET /api/restaurants
```

If the user adds a new restaurant from admin panel:

```http
POST /api/restaurants
```

If admin edits restaurant details:

```http
PUT /api/restaurants/2
```

If admin removes a restaurant:

```http
DELETE /api/restaurants/2
```

This is how real backend systems work every day.

## 4. Architecture Flow

```text
Client
  |
  v
RestaurantsController
  |
  v
IRestaurantService
  |
  v
RestaurantService
  |
  v
HTTP Response + JSON
```

## 5. Folder Structure

```text
02_HTTP_Basics/
  Project/
    SwiggyAPI.HttpBasics.API/
      Controllers/
        RestaurantsController.cs
      Models/
        Restaurant.cs
        RestaurantCreateRequest.cs
        RestaurantUpdateRequest.cs
      Services/
        ServiceCollectionExtensions.cs
      Interfaces/
        IRestaurantService.cs
      Implementations/
        RestaurantService.cs
      Program.cs
  Explanation_Topic/
    Practice_HTTP_Protocol_Explanation.md
  Interview_QA/
    Practice_HTTP_Protocol_Interview_QA.md
  Exercises/
    Practice_HTTP_Protocol_Exercises.md
```

## 6. Step-by-Step Implementation

### Step 1: Keep the same architecture

We continued the same simple flow:

- Controller
- Interface
- Service Implementation

This keeps the project consistent and beginner friendly.

### Step 2: Add restaurant request models

We added:

- `RestaurantCreateRequest`
- `RestaurantUpdateRequest`

Why?

Because HTTP `POST` and `PUT` usually receive request body data from client.

### Step 3: Add HTTP methods in controller

We added:

- `GET` for read all restaurants
- `GET` by id for read one restaurant
- `POST` for create
- `PUT` for update
- `DELETE` for remove

### Step 4: Implement service methods

Service now supports all basic HTTP operations using static list data.

No database is used yet.

## 7. Full Code

Main files:

- `Program.cs`
- `RestaurantsController.cs`
- `IRestaurantService.cs`
- `RestaurantService.cs`
- `Restaurant.cs`
- `RestaurantCreateRequest.cs`
- `RestaurantUpdateRequest.cs`

These files are inside the project folder and demonstrate full HTTP practice.

## 8. Code Explanation

### `GET`

Used to read data.

Examples:

- `GET /api/restaurants`
- `GET /api/restaurants/1`

### `POST`

Used to create new data.

Example:

```http
POST /api/restaurants
Content-Type: application/json
```

Request body:

```json
{
  "name": "City Foods",
  "city": "Chennai",
  "cuisine": "Multi Cuisine",
  "rating": 4.1,
  "isOpen": true
}
```

### `PUT`

Used to update an existing record.

Example:

```http
PUT /api/restaurants/1
```

### `DELETE`

Used to remove a record.

Example:

```http
DELETE /api/restaurants/1
```

## 9. Request Lifecycle

### Example: `POST /api/restaurants`

1. Client sends HTTP POST request
2. ASP.NET Core routing matches `RestaurantsController`
3. Request body is converted into `RestaurantCreateRequest`
4. Controller calls `_restaurantService.AddRestaurant(request)`
5. Service creates a new restaurant object
6. Service adds it to static list
7. Controller returns `201 Created`
8. Client receives created record as JSON

### Example: `DELETE /api/restaurants/2`

1. Client sends HTTP DELETE request
2. Route value `2` is passed to controller
3. Controller calls service
4. Service searches static list using LINQ
5. If found, record is removed
6. Controller returns success message

## 10. Interview Questions

### Beginner

What is HTTP?

HTTP is the protocol used for communication between client and server.

### Intermediate

What is the difference between GET and POST?

- `GET` reads data
- `POST` creates data

### Advanced

Why is using correct HTTP verb important in API design?

It keeps APIs predictable, readable, maintainable, and closer to real REST standards.

### Real-time scenario

If a delete request returns 404, what does it mean?

It usually means the requested resource was not found.

## 11. Exercises

1. Add one more restaurant using `POST`.
2. Update restaurant city using `PUT`.
3. Delete one restaurant using `DELETE`.
4. Try getting deleted restaurant again and observe `404`.
5. Explain in your own words why `GET` should not be used for delete operations.

## 12. Common Errors

- using wrong HTTP method
- forgetting route id in `PUT` or `DELETE`
- sending wrong JSON property names
- not checking null before update or delete
- writing create and update logic inside controller instead of service

## 13. Debugging Tips

- check request URL carefully
- check whether HTTP method is correct
- verify JSON body format
- put breakpoint in controller action
- put breakpoint in service method
- if request fails, check route and model property names

## 14. Best Practices

- use correct HTTP verb
- keep controller thin
- keep business logic in service
- return proper status codes like `200`, `201`, `404`
- use simple request models for body data
- keep API routes readable and consistent

## 15. Summary Notes

- HTTP is the communication foundation of Web API
- `GET` reads data
- `POST` creates data
- `PUT` updates data
- `DELETE` removes data
- controller handles request
- service handles logic
- proper HTTP usage is a core backend interview topic
