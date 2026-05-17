using SwiggyAPI.DependencyInjection.API.Models;

namespace SwiggyAPI.DependencyInjection.API.Interfaces;

public interface IRequestInfoService
{
    DependencyInfo GetDependencyInfo();
}
