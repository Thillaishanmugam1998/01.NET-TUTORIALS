using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.Hosting.API.Interfaces;
using SwiggyAPI.Hosting.API.Models;

namespace SwiggyAPI.Hosting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingController : ControllerBase
{
    private readonly IHostingInfoService _hostingInfoService;

    public HostingController(IHostingInfoService hostingInfoService)
    {
        // Dependency Injection gives the hosting service object automatically.
        _hostingInfoService = hostingInfoService;
    }

    [HttpGet("current")]
    public ActionResult<HostingInfo> GetCurrentHostingInfo()
    {
        // This endpoint helps us see simple runtime hosting details.
        var hostingInfo = _hostingInfoService.GetCurrentHostingInfo();
        return Ok(hostingInfo);
    }
}
