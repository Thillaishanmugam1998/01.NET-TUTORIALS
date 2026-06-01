using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodeSeriesDemo.API.Controllers;

[ApiController]
[Route("api/redirection-series")]
public class RedirectionSeriesController : ControllerBase
{
    [HttpGet("301-moved-permanently")]
    public IActionResult MovedPermanentlyExample()
    {
        // 301 Moved Permanently
        // Tells the client that the resource has permanently moved to a new URL.
        return RedirectPermanent("/api/successful-series/200-ok");
    }

    [HttpGet("302-found")]
    public IActionResult FoundExample()
    {
        // 302 Found
        // Temporary redirect. The client should use another URL for now.
        return Redirect("/api/successful-series/200-ok");
    }

    [HttpGet("304-not-modified")]
    public IActionResult NotModifiedExample()
    {
        // 304 Not Modified
        // Used with caching. It tells the client to use its cached version.
        return StatusCode(StatusCodes.Status304NotModified);
    }
}
