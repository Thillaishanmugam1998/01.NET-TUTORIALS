using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodeSeriesDemo.API.Controllers;

[ApiController]
[Route("api/successful-series")]
public class SuccessfulSeriesController : ControllerBase
{
    [HttpGet("200-ok")]
    public IActionResult OkExample()
    {
        // 200 OK
        // The request succeeded and the server is returning data.
        return Ok(new
        {
            StatusCode = 200,
            Message = "Request completed successfully."
        });
    }

    [HttpPost("201-created")]
    public IActionResult CreatedExample()
    {
        // 201 Created
        // Commonly used after a successful POST when a new resource is created.
        var createdResource = new
        {
            Id = 101,
            Name = "Sample Order"
        };

        return Created("/api/successful-series/201-created/101", createdResource);
    }

    [HttpPost("202-accepted")]
    public IActionResult AcceptedExample()
    {
        // 202 Accepted
        // The request is accepted, but processing has not finished yet.
        // Good fit for background jobs or queued operations.
        return Accepted(new
        {
            StatusCode = 202,
            Message = "Request accepted and queued for background processing."
        });
    }

    [HttpDelete("204-no-content")]
    public IActionResult NoContentExample()
    {
        // 204 No Content
        // The request succeeded, but there is no response body to send back.
        return NoContent();
    }
}
