using SwiggyAPI.Swagger.API.Interfaces;

namespace SwiggyAPI.Swagger.API.Implementations;

// This service creates a new id every time a new object instance is created.
public class TransientIdService : ITransientIdService
{
    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
}
