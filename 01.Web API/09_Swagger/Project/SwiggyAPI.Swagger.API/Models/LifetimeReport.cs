namespace SwiggyAPI.Swagger.API.Models;

public class LifetimeReport
{
    public string ServiceTransientId { get; set; } = string.Empty;
    public string ServiceScopedId { get; set; } = string.Empty;
    public string ServiceSingletonId { get; set; } = string.Empty;
}
