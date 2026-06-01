using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodeSeriesDemo.API.Controllers;

[ApiController]
[Route("api/server-error-series")]
public class ServerErrorSeriesController : ControllerBase
{
    [HttpGet("500-internal-server-error")]
    public IActionResult InternalServerErrorExample()
    {
        // 500 Internal Server Error
        // Generic server-side failure when something unexpected happens.
        return StatusCode(StatusCodes.Status500InternalServerError, new
        {
            StatusCode = 500,
            Message = "An unexpected server error occurred."
        });
    }

    [HttpGet("501-not-implemented")]
    public IActionResult NotImplementedExample()
    {
        // 501 Not Implemented
        // The server does not support the functionality required for the request.
        return StatusCode(StatusCodes.Status501NotImplemented, new
        {
            StatusCode = 501,
            Message = "This feature is not implemented yet."
        });
    }

    [HttpGet("503-service-unavailable")]
    public IActionResult ServiceUnavailableExample()
    {
        // 503 Service Unavailable
        // The server is temporarily unable to handle the request.
        return StatusCode(StatusCodes.Status503ServiceUnavailable, new
        {
            StatusCode = 503,
            Message = "Service is temporarily unavailable. Please try again later."
        });
    }
}
