using SwiggyAPI.DependencyInjection.API.Interfaces;
using SwiggyAPI.DependencyInjection.API.Models;

namespace SwiggyAPI.DependencyInjection.API.Implementations;

// This service returns simple Dependency Injection details.
public class RequestInfoService : IRequestInfoService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RequestInfoService(IWebHostEnvironment webHostEnvironment)
    {
        // Dependency Injection gives the current environment service automatically.
        _webHostEnvironment = webHostEnvironment;
    }

    public DependencyInfo GetDependencyInfo()
    {
        return new DependencyInfo
        {
            ServiceName = nameof(RequestInfoService),
            EnvironmentName = _webHostEnvironment.EnvironmentName,
            Message = "Dependency Injection creates and supplies required service objects automatically."
        };
    }
}
