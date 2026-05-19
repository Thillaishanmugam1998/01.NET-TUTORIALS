# 01 — What is this topic?

**Multiple URLs for a Single Resource** means we allow the **same data** to be reached through **more than one route** in ASP.NET Core Web API.

In simple English:
- One resource stays the same
- But clients can use different URL patterns to access it

### Swiggy analogy

Imagine one Swiggy order with id `1`.

You may reach that same order in different ways:
- `GET /api/orders/1`
- `GET /api/orders/details/1`
- `GET /api/orders/order-details/1`

All these URLs still mean: **show me the same order**.

> 💡 The resource is the same. Only the route pattern is different.

# 02 — Why do we need it?

Sometimes different clients, teams, or old API consumers use different URL styles.

Without multiple URLs:
- We may need to force everyone to change immediately
- Old links may break
- API migration becomes harder

With multiple URLs:
- We support **backward compatibility**
- We keep **older route styles working**
- We offer **friendly aliases** for learning or readability

## Before vs After

| Situation | Before | After |
|---|---|---|
| One order lookup | Only `/api/orders/1` works | `/api/orders/1`, `/api/orders/details/1`, and `/api/orders/order-details/1` all work |
| Old client support | Old URLs break after route change | Old and new URLs can work together |
| Readability | One fixed naming style only | We can add a clearer alias if needed |

# 03 — How it works

ASP.NET Core lets us place **multiple route attributes** on the **same action method**.

## Step-by-step

1. The client sends a request URL.
2. ASP.NET Core checks controller route attributes.
3. If any route pattern matches, ASP.NET Core calls the same action method.
4. The route value like `id` is passed to the method parameter.
5. The controller calls the service layer.
6. The service fetches the order from the static `List<Order>`.
7. The same resource is returned as JSON.

## Simple flow

```text
/api/orders/1
         \
/api/orders/details/1 ----> Same Controller Action ----> Same Service Method ----> Same Order Data
         /
/api/orders/order-details/1
```

## Example

```csharp
[HttpGet("{id:int}")]
[HttpGet("details/{id:int}")]
[HttpGet("order-details/{id:int}")]
public ActionResult<Order> GetOrderById(int id)
{
    var order = _orderService.GetOrderById(id);

    if (order is null)
    {
        return NotFound();
    }

    return Ok(order);
}
```

> ⚠️ Multiple routes are useful, but do not create too many aliases without a reason. It can make the API harder to maintain.

# 04 — Key concepts

| Term | Meaning | Example |
|---|---|---|
| **Resource** | The actual data we want to access | One Swiggy order |
| **Route Template** | The URL pattern written in an attribute | `[HttpGet("{id:int}")]` |
| **Alias Route** | Another URL that points to the same action | `[HttpGet("details/{id:int}")]` |
| **Route Constraint** | A rule for route values | `{id:int}` |
| **Action Method** | Controller method that handles the request | `GetOrderById(int id)` |
| **Backward Compatibility** | Keeping old clients working after changes | Supporting both old and new URLs |

# 05 — Real-world Swiggy example

Suppose Swiggy has one order resource.

Support team may use:
- `/api/orders/1`

An older mobile app may still call:
- `/api/orders/details/1`

A reporting tool may use:
- `/api/orders/order-details/1`

All three should return the same order:

```csharp
{
  "id": 1,
  "customerName": "Asha",
  "restaurantName": "Annapoorna",
  "deliveryArea": "Gandhipuram",
  "status": "Placed",
  "totalAmount": 250
}
```

This is helpful when:
- A route name changes over time
- Different teams prefer different naming
- We want a more descriptive URL without breaking the older one

For tracking also, we can support:
- `/api/orders/1/track`
- `/api/orders/track-order/1`

Both URLs still return the same tracking message for the same order.

# 06 — Code walkthrough

## 1. `Program.cs`

No major change was needed here.

```csharp
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IOrderService, OrderService>();
```

This still:
- Enables controllers
- Enables OpenAPI
- Registers `IOrderService` with `OrderService`

## 2. `Services/IOrderService.cs`

The interface contract remains the same because the **resource-fetching logic** is still the same.

```csharp
Order? GetOrderById(int id);
string? TrackOrder(int id);
```

Why no new method?
- Because multiple URLs are a **routing feature**
- The service still fetches the same order in the same way

## 3. `Services/OrderService.cs`

This file still contains the actual business logic.

```csharp
public Order? GetOrderById(int id)
{
    // Find the first order whose Id matches the route value.
    return Orders.FirstOrDefault(order => order.Id == id);
}
```

For tracking:

```csharp
public string? TrackOrder(int id)
{
    var existingOrder = GetOrderById(id);

    if (existingOrder is null)
    {
        return null;
    }

    return $"Order {existingOrder.Id} for {existingOrder.CustomerName} is currently {existingOrder.Status}.";
}
```

## 4. `Controllers/OrdersController.cs`

This is the main topic file for this lesson.

### Same order with multiple URLs

```csharp
[HttpGet("{id:int}")]
[HttpGet("details/{id:int}")]
[HttpGet("order-details/{id:int}")]
public ActionResult<Order> GetOrderById(int id)
{
    // These three route templates point to the same action.
    var order = _orderService.GetOrderById(id);

    if (order is null)
    {
        return NotFound($"Order with id {id} was not found.");
    }

    return Ok(order);
}
```

What this means:
- `GET /api/orders/1` works
- `GET /api/orders/details/1` works
- `GET /api/orders/order-details/1` works
- But all of them call **the same method**

### Same tracking resource with multiple URLs

```csharp
[HttpGet("{id:int}/track")]
[HttpGet("track-order/{id:int}")]
public ActionResult<string> TrackOrder(int id)
{
    var trackingMessage = _orderService.TrackOrder(id);

    if (trackingMessage is null)
    {
        return NotFound($"Order with id {id} was not found.");
    }

    return Ok(trackingMessage);
}
```

## 5. `SwiggyRelated.API.http`

This file now contains test URLs for the route aliases.

```csharp
GET /api/orders/1
GET /api/orders/details/1
GET /api/orders/order-details/1

GET /api/orders/1/track
GET /api/orders/track-order/1
```

> 💡 This helps you test that many URLs can point to one resource without changing the service logic.

# 07 — Common mistakes

> ⚠️ **Mistake 1:** Creating different URLs that accidentally return different data  
If the topic is about one resource with multiple URLs, all alias URLs should return the same result.

> ⚠️ **Mistake 2:** Adding too many route aliases  
Use aliases only when there is a clear need like backward compatibility or better readability.

> ⚠️ **Mistake 3:** Forgetting route constraints  
Use `{id:int}` so only numeric ids match the route.

> ⚠️ **Mistake 4:** Thinking this needs a new service method every time  
Usually this is a **controller routing change**, not a business logic change.

> ⚠️ **Mistake 5:** Breaking the original route  
When adding an alternate URL, keep the main route working unless you intentionally want to remove it.

# 08 — Quick summary

**Multiple URLs for a single resource means one ASP.NET Core action can be mapped to more than one route so the same Swiggy order or tracking data can be reached by different URL patterns. This is useful for backward compatibility, readable aliases, and gradual API changes. In this topic, we kept the same controller -> interface -> service structure and added multiple route attributes to the same action methods so different URLs return the same resource.**
