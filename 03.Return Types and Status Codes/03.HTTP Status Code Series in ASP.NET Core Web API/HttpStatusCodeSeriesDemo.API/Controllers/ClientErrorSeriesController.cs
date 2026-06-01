using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodeSeriesDemo.API.Controllers;

[ApiController]
[Route("api/client-error-series")]
public class ClientErrorSeriesController : ControllerBase
{
    [HttpGet("400-bad-request")]
    public IActionResult BadRequestExample()
    {
        // 400 Bad Request
        // The client sent invalid or incomplete data.
        return BadRequest(new
        {
            StatusCode = 400,
            Message = "The request data is invalid."
        });
    }

    [HttpGet("401-unauthorized")]
    public IActionResult UnauthorizedExample()
    {
        // 401 Unauthorized
        // The request needs authentication.
        return Unauthorized(new
        {
            StatusCode = 401,
            Message = "Authentication is required."
        });
    }

    [HttpGet("403-forbidden")]
    public IActionResult ForbiddenExample()
    {
        // 403 Forbidden
        // The user is authenticated, but does not have permission to access the resource.
        return Forbid();
    }

    [HttpGet("404-not-found")]
    public IActionResult NotFoundExample()
    {
        // 404 Not Found
        // The requested resource does not exist.
        return NotFound(new
        {
            StatusCode = 404,
            Message = "Requested resource was not found."
        });
    }

    [HttpGet("409-conflict")]
    public IActionResult ConflictExample()
    {
        // 409 Conflict
        // Used when the request conflicts with the current resource state.
        return Conflict(new
        {
            StatusCode = 409,
            Message = "A resource with the same unique value already exists."
        });
    }
}
