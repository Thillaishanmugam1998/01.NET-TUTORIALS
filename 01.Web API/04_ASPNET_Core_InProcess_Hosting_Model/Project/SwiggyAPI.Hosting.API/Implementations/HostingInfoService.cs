using System.Diagnostics;
using SwiggyAPI.Hosting.API.Interfaces;
using SwiggyAPI.Hosting.API.Models;

namespace SwiggyAPI.Hosting.API.Implementations;

// This service returns simple hosting-related runtime information.
public class HostingInfoService : IHostingInfoService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HostingInfoService(IWebHostEnvironment webHostEnvironment)
    {
        // ASP.NET Core injects current environment details automatically.
        _webHostEnvironment = webHostEnvironment;
    }

    public HostingInfo GetCurrentHostingInfo()
    {
        var currentProcess = Process.GetCurrentProcess();

        return new HostingInfo
        {
            ProcessId = Environment.ProcessId,
            ProcessName = currentProcess.ProcessName,
            EnvironmentName = _webHostEnvironment.EnvironmentName,
            MachineName = Environment.MachineName,
            HostingModelNote = "When deployed to IIS using in-process hosting, the ASP.NET Core app runs inside the IIS worker process."
        };
    }
}
