namespace SwiggyAPI.OutOfProcessHosting.API.Models;

public class HostingInfo
{
    public int ProcessId { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public string EnvironmentName { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string WebServerName { get; set; } = string.Empty;
    public string HostingModelNote { get; set; } = string.Empty;
}
