using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.DependencyInjection.API.Interfaces;
using SwiggyAPI.DependencyInjection.API.Models;

namespace SwiggyAPI.DependencyInjection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DependencyController : ControllerBase
{
    private readonly IRequestInfoService _requestInfoService;

    public DependencyController(IRequestInfoService requestInfoService)
    {
        // Dependency Injection gives the required service object automatically.
        _requestInfoService = requestInfoService;
    }

    [HttpGet("current")]
    public ActionResult<DependencyInfo> GetCurrentDependencyInfo()
    {
        // This endpoint helps us see simple Dependency Injection details.
        var dependencyInfo = _requestInfoService.GetDependencyInfo();
        return Ok(dependencyInfo);
    }
}
