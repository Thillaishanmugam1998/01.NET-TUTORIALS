using SwiggyAPI.ServiceLifetimes.API.Interfaces;

namespace SwiggyAPI.ServiceLifetimes.API.Implementations;

// This service keeps the same id for one HTTP request.
public class ScopedIdService : IScopedIdService
{
    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
}
