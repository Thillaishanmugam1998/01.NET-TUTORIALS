using System.Diagnostics;
using SwiggyAPI.Kestrel.API.Interfaces;
using SwiggyAPI.Kestrel.API.Models;

namespace SwiggyAPI.Kestrel.API.Implementations;

// This service returns simple Kestrel-related runtime information.
public class ServerInfoService : IServerInfoService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ServerInfoService(IWebHostEnvironment webHostEnvironment)
    {
        // ASP.NET Core injects current environment details automatically.
        _webHostEnvironment = webHostEnvironment;
    }

    public ServerInfo GetCurrentServerInfo()
    {
        var currentProcess = Process.GetCurrentProcess();

        return new ServerInfo
        {
            ServerName = "Kestrel",
            ProcessId = Environment.ProcessId,
            ProcessName = currentProcess.ProcessName,
            EnvironmentName = _webHostEnvironment.EnvironmentName,
            MachineName = Environment.MachineName,
            ServerNote = "Kestrel is the default cross-platform web server for ASP.NET Core applications."
        };
    }
}
