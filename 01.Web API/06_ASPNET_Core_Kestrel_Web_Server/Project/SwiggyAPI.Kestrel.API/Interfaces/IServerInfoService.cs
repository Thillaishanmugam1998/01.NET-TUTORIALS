using SwiggyAPI.Kestrel.API.Models;

namespace SwiggyAPI.Kestrel.API.Interfaces;

public interface IServerInfoService
{
    ServerInfo GetCurrentServerInfo();
}
