using System.Diagnostics;
using SwiggyAPI.OutOfProcessHosting.API.Interfaces;
using SwiggyAPI.OutOfProcessHosting.API.Models;

namespace SwiggyAPI.OutOfProcessHosting.API.Implementations;

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
            WebServerName = "Kestrel",
            HostingModelNote = "When deployed to IIS using out-of-process hosting, IIS forwards requests to a separate ASP.NET Core process running on Kestrel."
        };
    }
}
