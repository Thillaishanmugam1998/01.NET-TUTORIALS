using SwiggyAPI.Swagger.API.Interfaces;
using SwiggyAPI.Swagger.API.Models;

namespace SwiggyAPI.Swagger.API.Implementations;

// This service collects service lifetime ids from another class so we can compare them.
public class LifetimeReportService : ILifetimeReportService
{
    private readonly ITransientIdService _transientIdService;
    private readonly IScopedIdService _scopedIdService;
    private readonly ISingletonIdService _singletonIdService;

    public LifetimeReportService(
        ITransientIdService transientIdService,
        IScopedIdService scopedIdService,
        ISingletonIdService singletonIdService)
    {
        // Dependency Injection gives these services automatically based on their configured lifetimes.
        _transientIdService = transientIdService;
        _scopedIdService = scopedIdService;
        _singletonIdService = singletonIdService;
    }

    public LifetimeReport GetLifetimeReport()
    {
        return new LifetimeReport
        {
            ServiceTransientId = _transientIdService.InstanceId,
            ServiceScopedId = _scopedIdService.InstanceId,
            ServiceSingletonId = _singletonIdService.InstanceId
        };
    }
}
