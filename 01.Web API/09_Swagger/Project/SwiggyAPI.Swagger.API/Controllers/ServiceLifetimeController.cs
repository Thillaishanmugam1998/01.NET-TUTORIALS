using Microsoft.AspNetCore.Mvc;
using SwiggyAPI.Swagger.API.Interfaces;
using SwiggyAPI.Swagger.API.Models;

namespace SwiggyAPI.Swagger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceLifetimeController : ControllerBase
{
    private readonly ITransientIdService _transientIdServiceOne;
    private readonly ITransientIdService _transientIdServiceTwo;
    private readonly IScopedIdService _scopedIdServiceOne;
    private readonly IScopedIdService _scopedIdServiceTwo;
    private readonly ISingletonIdService _singletonIdServiceOne;
    private readonly ISingletonIdService _singletonIdServiceTwo;
    private readonly ILifetimeReportService _lifetimeReportService;

    public ServiceLifetimeController(
        ITransientIdService transientIdServiceOne,
        ITransientIdService transientIdServiceTwo,
        IScopedIdService scopedIdServiceOne,
        IScopedIdService scopedIdServiceTwo,
        ISingletonIdService singletonIdServiceOne,
        ISingletonIdService singletonIdServiceTwo,
        ILifetimeReportService lifetimeReportService)
    {
        // Two constructor parameters of the same service type help us compare lifetimes clearly.
        _transientIdServiceOne = transientIdServiceOne;
        _transientIdServiceTwo = transientIdServiceTwo;
        _scopedIdServiceOne = scopedIdServiceOne;
        _scopedIdServiceTwo = scopedIdServiceTwo;
        _singletonIdServiceOne = singletonIdServiceOne;
        _singletonIdServiceTwo = singletonIdServiceTwo;
        _lifetimeReportService = lifetimeReportService;
    }

    [HttpGet("compare")]
    public ActionResult<ServiceLifetimeInfo> CompareServiceLifetimes()
    {
        // This endpoint helps us compare singleton, scoped, and transient behavior in one request.
        var reportFromService = _lifetimeReportService.GetLifetimeReport();

        var response = new ServiceLifetimeInfo
        {
            ControllerTransientIdOne = _transientIdServiceOne.InstanceId,
            ControllerTransientIdTwo = _transientIdServiceTwo.InstanceId,
            ControllerScopedIdOne = _scopedIdServiceOne.InstanceId,
            ControllerScopedIdTwo = _scopedIdServiceTwo.InstanceId,
            ControllerSingletonIdOne = _singletonIdServiceOne.InstanceId,
            ControllerSingletonIdTwo = _singletonIdServiceTwo.InstanceId,
            ServiceTransientId = reportFromService.ServiceTransientId,
            ServiceScopedId = reportFromService.ServiceScopedId,
            ServiceSingletonId = reportFromService.ServiceSingletonId,
            Explanation = "Transient usually changes for each resolution. Scoped stays same within one request. Singleton stays same for the entire application lifetime."
        };

        return Ok(response);
    }
}
