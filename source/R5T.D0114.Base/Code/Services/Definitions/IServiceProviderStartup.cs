using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0064;


namespace R5T.D0114
{
    [ServiceDefinitionMarker]
    public interface IServiceProviderStartup : IServiceDefinition
    {
        Task ConfigureServices(IServiceCollection services);
    }
}