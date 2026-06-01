using Microsoft.AspNetCore.Mvc;

namespace HttpStatusCodeSeriesDemo.API.Controllers;

[ApiController]
[Route("api/informational-series")]
public class InformationalSeriesController : ControllerBase
{
    [HttpGet("100-continue")]
    public IActionResult ContinueExample()
    {
        // 100 Continue
        // This means the client can continue sending the request body.
        // In real APIs this is usually handled by the HTTP pipeline, not by controller actions.
        return StatusCode(StatusCodes.Status100Continue, new
        {
            StatusCode = 100,
            Message = "Continue sending the request body."
        });
    }

    [HttpGet("101-switching-protocols")]
    public IActionResult SwitchingProtocolsExample()
    {
        // 101 Switching Protocols
        // Used when the server agrees to switch protocols, for example HTTP to WebSocket.
        // This is uncommon in normal REST API controller methods.
        return StatusCode(StatusCodes.Status101SwitchingProtocols, new
        {
            StatusCode = 101,
            Message = "Server accepted the protocol switch request."
        });
    }

    [HttpGet("102-processing")]
    public IActionResult ProcessingExample()
    {
        // 102 Processing
        // Indicates the server has received the request and is still processing it.
        return StatusCode(StatusCodes.Status102Processing, new
        {
            StatusCode = 102,
            Message = "Request accepted and is still being processed."
        });
    }
}
