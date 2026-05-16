# Practice HTTP Protocol - Interview Q&A

## Beginner Questions

### 1. What is HTTP?

HTTP is a communication protocol used between client and server.

Real-time usage:
Every web app, mobile app, and API call uses HTTP or HTTPS to communicate with backend systems.

Common mistake:
Thinking HTTP is only for web pages. It is also the base for backend APIs.

### 2. What is an HTTP request?

An HTTP request is the message sent by client to server.

It contains:

- method
- URL
- headers
- optional body

Common mistake:
Ignoring headers and body while testing APIs.

### 3. What is an HTTP response?

An HTTP response is the message sent back by server to client.

It contains:

- status code
- headers
- response body

## Intermediate Questions

### 4. What is the difference between GET and POST?

`GET` is used to read data.
`POST` is used to create new data.

Real-time usage:
Restaurant listing uses `GET`, and admin create operation uses `POST`.

Common mistake:
Using `GET` to create or delete data.

### 5. What is the difference between PUT and DELETE?

`PUT` updates an existing resource.
`DELETE` removes an existing resource.

Real-time usage:
Updating restaurant timing uses `PUT`.
Removing an inactive restaurant uses `DELETE`.

### 6. What are status codes?

Status codes tell the result of the request.

Examples:

- `200 OK`
- `201 Created`
- `404 Not Found`
- `500 Internal Server Error`

Common mistake:
Returning `200` for every scenario even when resource is missing.

## Advanced Questions

### 7. Why should APIs use proper HTTP verbs?

Because correct HTTP verbs make APIs more standard, predictable, and easy to understand.

Real-time usage:
Enterprise APIs follow this for frontend teams, partner integrations, and documentation tools.

### 8. Why do we return `201 Created` after POST?

Because a new resource was successfully created.

In many cases, API also returns the created object and location details.

Common mistake:
Returning only `200 OK` after creation when `201 Created` is more meaningful.

### 9. Why is `404 Not Found` important?

It clearly tells the client that requested resource does not exist.

Real-time usage:
Frontend can use this to show proper message to user.

## Real-Time Scenario Questions

### 10. A frontend developer says delete is not working. What will you check first?

Check:

- request URL
- HTTP method
- route id
- whether record exists
- controller action hit or not

### 11. Why do we use separate request models for POST and PUT?

Because request body shape is often different from response model or database model.

Real-time usage:
This gives better control and cleaner API contracts.

### 12. If client sends wrong JSON body, what kind of issue can happen?

The model may not bind correctly, values may be empty, or validation can fail.

Common mistake:
Assuming request body always arrives correctly without checking model shape.

## Quick Revision Points

- HTTP is the base protocol for APIs
- request goes from client to server
- response comes from server to client
- `GET` reads
- `POST` creates
- `PUT` updates
- `DELETE` removes
- status codes tell result
