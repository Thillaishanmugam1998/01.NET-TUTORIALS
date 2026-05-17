using SwiggyAPI.Swagger.API.Interfaces;

namespace SwiggyAPI.Swagger.API.Implementations;

// This service keeps the same id for one HTTP request.
public class ScopedIdService : IScopedIdService
{
    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
}
