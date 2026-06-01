using ActionReturnTypesDemo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionReturnTypesDemo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // ============================================================
    // In-memory data — No database needed for this tutorial.
    // Goal: Learn all Action Return Types clearly.
    // ============================================================
    private static readonly List<Product> Products =
    [
        new() { Id = 1, Name = "Laptop",   Price = 75000, IsActive = true  },
        new() { Id = 2, Name = "Mouse",    Price = 1200,  IsActive = true  },
        new() { Id = 3, Name = "Keyboard", Price = 2500,  IsActive = false }
    ];

    private static readonly Customer SampleCustomer = new()
    {
        Id = 101,
        FullName = "Ravi Kumar",
        City = "Chennai"
    };


    // ============================================================
    // RETURN TYPE 1 : Primitive Type (int, string, bool)
    // ============================================================
    // WHEN TO USE : Always succeeds, returns one simple value.
    // BENEFIT     : Very simple, no wrapper needed.
    // LIMITATION  : Cannot return 404 / 400 without changing return type.
    // RESPONSE    : 200 OK  →  3
    // ============================================================
    [HttpGet("count")]
    public int GetProductsCount()
    {
        return Products.Count;
    }


    // ============================================================
    // RETURN TYPE 2 : Complex Object (Product, Customer, etc.)
    // ============================================================
    // WHEN TO USE : Action always succeeds, returns one model object.
    // BENEFIT     : Easy to read, Swagger shows exact model shape.
    // LIMITATION  : Cannot return different HTTP status codes.
    // RESPONSE    : 200 OK  →  { "id": 1, "name": "Laptop", ... }
    // ============================================================
    [HttpGet("first-product")]
    public Product GetFirstProduct()
    {
        return Products[0];
    }

    [HttpGet("sample-customer")]
    public Customer GetSampleCustomer()
    {
        // Any CLR object can be returned — not only Product.
        return SampleCustomer;
    }


    // ============================================================
    // RETURN TYPE 3 : IActionResult
    // ============================================================
    // WHEN TO USE : Multiple HTTP responses possible (200, 400, 404).
    // BENEFIT     : Maximum flexibility — Ok(), NotFound(), BadRequest(),
    //               Created(), NoContent(), etc. எல்லாம் return பண்ணலாம்.
    // LIMITATION  : Swagger-க்கு success data type தெரியாது.
    //               Caller-க்கு "some action result" மட்டும் தெரியும்.
    // ============================================================
    [HttpGet("{id}/http-aware")]
    public IActionResult GetProductHttpAware(int id)
    {
        if (id <= 0)
        {
            // 400 Bad Request — invalid input
            return BadRequest("Product id must be greater than 0.");
        }

        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            // 404 Not Found — record இல்ல
            return NotFound($"No product found with id {id}.");
        }

        // 200 OK — success
        return Ok(product);
    }


    // ============================================================
    // RETURN TYPE 4 : ActionResult<T>  — Single Object
    // ============================================================
    // WHEN TO USE : Multiple HTTP responses + success type clearly known.
    // BENEFIT     : 1. Product directly return பண்ணலாம் (no Ok() wrapper தேவையில்ல)
    //               2. NotFound(), BadRequest() also return பண்ணலாம்.
    //               3. Swagger-ல success response type clearly தெரியும்.
    // LIMITATION  : Beginners-க்கு IActionResult-ஐ விட கொஞ்சம் confusing.
    // ============================================================
    [HttpGet("{id}/action-result")]
    public ActionResult<Product> GetProductUsingActionResult(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Product id must be greater than 0.");
        }

        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound($"No product found with id {id}.");
        }

        // Product directly return — Ok() wrap பண்ண வேண்டாம்
        return product;
    }


    // ============================================================
    // RETURN TYPE 5 : ActionResult<IEnumerable<T>>  — Collection
    // ============================================================
    // WHEN TO USE : List return பண்ணும்போது, HTTP flexibility வேணும்போது.
    // BENEFIT     : Collection + error responses both handle பண்ணலாம்.
    // NOTE        : IEnumerable — loop மட்டும், .Count இல்ல.
    // ============================================================
    [HttpGet("active")]
    public ActionResult<IEnumerable<Product>> GetActiveProducts()
    {
        var activeProducts = Products.Where(p => p.IsActive).ToList();

        if (activeProducts.Count == 0)
        {
            return NotFound("There are no active products.");
        }

        return activeProducts;
    }


    // ============================================================
    // RETURN TYPE 6 : ActionResult<List<T>>  — Collection
    // ============================================================
    // WHEN TO USE : .Count, .Add, .Remove use பண்ணணும்போது.
    // BENEFIT     : IEnumerable-ஐ விட more methods available.
    // NOTE        : Client-க்கு போற JSON — IEnumerable-உம் List-உம் same array.
    // ============================================================
    [HttpGet("inactive")]
    public ActionResult<List<Product>> GetInactiveProducts()
    {
        var inactiveProducts = Products.Where(p => !p.IsActive).ToList();

        if (inactiveProducts.Count == 0)
        {
            return NotFound("There are no inactive products.");
        }

        return inactiveProducts;
    }


    // ============================================================
    // RETURN TYPE 7 : Task<T>  — Async Primitive
    // ============================================================
    // WHEN TO USE : Async operation, always succeeds, simple value return.
    // BENEFIT     : Non-blocking — thread free ஆகுது, DB/API call-க்கு best.
    // NOTE        : await Task.FromResult() — real project-ல DB call வரும்.
    // RESPONSE    : 200 OK  →  3
    // ============================================================
    [HttpGet("count-async")]
    public async Task<int> GetProductsCountAsync()
    {
        // Real project-ல: return await _dbContext.Products.CountAsync();
        var count = await Task.FromResult(Products.Count);
        return count;
    }


    // ============================================================
    // RETURN TYPE 8 : Task<IActionResult>  — Async + HTTP Flexible
    // ============================================================
    // WHEN TO USE : Async + multiple HTTP responses possible.
    // BENEFIT     : Non-blocking + Ok(), NotFound(), BadRequest() எல்லாம் OK.
    // LIMITATION  : Swagger-க்கு success type தெரியாது (IActionResult போல).
    // ============================================================
    [HttpGet("{id}/async-http-aware")]
    public async Task<IActionResult> GetProductAsyncHttpAware(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Product id must be greater than 0.");
        }

        // Real project-ல: var product = await _dbContext.Products.FindAsync(id);
        var product = await Task.FromResult(
            Products.FirstOrDefault(p => p.Id == id)
        );

        if (product is null)
        {
            return NotFound($"No product found with id {id}.");
        }

        return Ok(product);
    }


    // ============================================================
    // RETURN TYPE 9 : Task<ActionResult<T>>  — Async + Strongly Typed
    // ============================================================
    // WHEN TO USE : Real-world Web API standard approach.
    //               Async DB call + multiple HTTP responses + type safety.
    // BENEFIT     : 1. Non-blocking async
    //               2. NotFound(), BadRequest() return பண்ணலாம்
    //               3. Swagger-ல success type clearly தெரியும்
    //               4. Product directly return — Ok() வேண்டாம்
    // ✅ RECOMMENDED : இதுவே modern Web API-ல best practice.
    // ============================================================
    [HttpGet("{id}/async-action-result")]
    public async Task<ActionResult<Product>> GetProductAsyncActionResult(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Product id must be greater than 0.");
        }

        // Real project-ல: var product = await _dbContext.Products.FindAsync(id);
        var product = await Task.FromResult(
            Products.FirstOrDefault(p => p.Id == id)
        );

        if (product is null)
        {
            return NotFound($"No product found with id {id}.");
        }

        return product;
    }


    // ============================================================
    // RETURN TYPE 10 : Task<ActionResult<List<T>>>  — Async Collection
    // ============================================================
    // WHEN TO USE : Async list fetch + HTTP flexibility வேணும்போது.
    // BENEFIT     : Real DB call-ல ToListAsync() use பண்ணலாம்.
    // ✅ RECOMMENDED for list endpoints in real projects.
    // ============================================================
    [HttpGet("active-async")]
    public async Task<ActionResult<List<Product>>> GetActiveProductsAsync()
    {
        // Real project-ல:
        // var activeProducts = await _dbContext.Products
        //     .Where(p => p.IsActive)
        //     .ToListAsync();
        var activeProducts = await Task.FromResult(
            Products.Where(p => p.IsActive).ToList()
        );

        if (activeProducts.Count == 0)
        {
            return NotFound("There are no active products.");
        }

        return activeProducts;
    }


    // ============================================================
    // RETURN TYPE 11 : IActionResult  — NoContent (204)
    // ============================================================
    // WHEN TO USE : Delete / Update — success-ஆ ஆச்சு, return data இல்ல.
    // RESPONSE    : 204 No Content (body இல்லாம்)
    // ============================================================
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound($"No product found with id {id}.");
        }

        Products.Remove(product);

        // 204 No Content — deleted, nothing to return
        return NoContent();
    }


    // ============================================================
    // RETURN TYPE 12 : ActionResult<T>  — CreatedAtAction (201)
    // ============================================================
    // WHEN TO USE : POST — new record create பண்ணும்போது.
    // RESPONSE    : 201 Created + Location header + created object
    // ============================================================
    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
    {
        if (newProduct is null)
        {
            return BadRequest("Product data is required.");
        }

        newProduct.Id = Products.Max(p => p.Id) + 1;
        Products.Add(newProduct);

        // 201 Created — Location: api/products/{id}
        return CreatedAtAction(
            actionName: nameof(GetProductUsingActionResult),
            routeValues: new { id = newProduct.Id },
            value: newProduct
        );
    }
}