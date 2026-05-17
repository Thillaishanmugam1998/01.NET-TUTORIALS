using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.Kestrel.API.Interfaces;
using SwiggyAPI.Kestrel.API.Models;

namespace SwiggyAPI.Kestrel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerController : ControllerBase
{
    private readonly IServerInfoService _serverInfoService;

    public ServerController(IServerInfoService serverInfoService)
    {
        // Dependency Injection gives the server info service object automatically.
        _serverInfoService = serverInfoService;
    }

    [HttpGet("current")]
    public ActionResult<ServerInfo> GetCurrentServerInfo()
    {
        // This endpoint helps us see simple Kestrel-related runtime details.
        var serverInfo = _serverInfoService.GetCurrentServerInfo();
        return Ok(serverInfo);
    }
}
