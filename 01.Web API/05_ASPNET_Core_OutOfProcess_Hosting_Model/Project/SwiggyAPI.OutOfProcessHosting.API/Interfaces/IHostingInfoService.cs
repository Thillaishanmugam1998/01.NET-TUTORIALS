using SwiggyAPI.OutOfProcessHosting.API.Models;

namespace SwiggyAPI.OutOfProcessHosting.API.Interfaces;

public interface IHostingInfoService
{
    HostingInfo GetCurrentHostingInfo();
}
