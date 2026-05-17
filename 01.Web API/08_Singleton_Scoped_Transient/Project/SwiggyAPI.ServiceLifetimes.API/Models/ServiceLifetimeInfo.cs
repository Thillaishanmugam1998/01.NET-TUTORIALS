namespace SwiggyAPI.ServiceLifetimes.API.Models;

public class ServiceLifetimeInfo
{
    public string ControllerTransientIdOne { get; set; } = string.Empty;
    public string ControllerTransientIdTwo { get; set; } = string.Empty;
    public string ControllerScopedIdOne { get; set; } = string.Empty;
    public string ControllerScopedIdTwo { get; set; } = string.Empty;
    public string ControllerSingletonIdOne { get; set; } = string.Empty;
    public string ControllerSingletonIdTwo { get; set; } = string.Empty;
    public string ServiceTransientId { get; set; } = string.Empty;
    public string ServiceScopedId { get; set; } = string.Empty;
    public string ServiceSingletonId { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
}
