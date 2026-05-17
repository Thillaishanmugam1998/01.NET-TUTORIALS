namespace SwiggyAPI.Kestrel.API.Models;

public class ServerInfo
{
    public string ServerName { get; set; } = string.Empty;
    public int ProcessId { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public string EnvironmentName { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string ServerNote { get; set; } = string.Empty;
}
