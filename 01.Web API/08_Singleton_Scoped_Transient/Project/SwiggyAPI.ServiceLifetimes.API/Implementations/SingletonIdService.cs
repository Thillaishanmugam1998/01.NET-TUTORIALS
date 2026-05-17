using SwiggyAPI.ServiceLifetimes.API.Interfaces;

namespace SwiggyAPI.ServiceLifetimes.API.Implementations;

// This service keeps the same id for the full application lifetime.
public class SingletonIdService : ISingletonIdService
{
    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
}
