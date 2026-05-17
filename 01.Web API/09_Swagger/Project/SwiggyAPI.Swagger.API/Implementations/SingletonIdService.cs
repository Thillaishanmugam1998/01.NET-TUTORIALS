using SwiggyAPI.Swagger.API.Interfaces;

namespace SwiggyAPI.Swagger.API.Implementations;

// This service keeps the same id for the full application lifetime.
public class SingletonIdService : ISingletonIdService
{
    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
}
