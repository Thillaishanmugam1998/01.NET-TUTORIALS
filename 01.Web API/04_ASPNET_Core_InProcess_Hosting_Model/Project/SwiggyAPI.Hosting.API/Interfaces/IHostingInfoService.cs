using SwiggyAPI.Hosting.API.Models;

namespace SwiggyAPI.Hosting.API.Interfaces;

public interface IHostingInfoService
{
    HostingInfo GetCurrentHostingInfo();
}
