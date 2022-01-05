using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;
using R5T.T0072;

using Instances = R5T.D0114.X001.Instances;


// Namespace chosen to be specific on purpose.
namespace R5T.D0114
{
    public static class IServiceProviderStartupExtensions
    {
        public static TServiceProviderBuilder UseServiceProviderStartup<TServiceProviderStartup, TServiceProviderBuilder>(this TServiceProviderBuilder serviceProviderBuilder,
            IServiceAction<TServiceProviderStartup> serviceProviderStartupAction)
            where TServiceProviderStartup : IServiceProviderStartup
            where TServiceProviderBuilder : 
                IHasConfigureServices<TServiceProviderBuilder>
        {
            var startupServiceProvider = Instances.ServiceOperator.GetServiceInstance(
                serviceProviderStartupAction,
                out var serviceProviderStartup);

            serviceProviderBuilder.UseServiceProviderStartup(serviceProviderStartup);

            // Add a ConfigureServices() call(added after the startup instance's ConfigureServices() call) that will dispose of the startup service provider now that the startup service will have been used.
            serviceProviderBuilder.ConfigureServices(_ =>
            {
                startupServiceProvider.Dispose(); // Chose Dispose() over DisposeAsync().
            });

            return serviceProviderBuilder;
        }

        public static TServiceProviderBuilder UseServiceProviderStartup<TServiceProviderStartup, TServiceProviderBuilder>(this TServiceProviderBuilder serviceProviderBuilder,
            TServiceProviderStartup serviceProviderStartup)
            where TServiceProviderStartup : IServiceProviderStartup
            where TServiceProviderBuilder :
                IHasConfigureServices<TServiceProviderBuilder>
        {
            serviceProviderBuilder
                .ConfigureServices(services =>
                {
                    SyncOverAsyncHelper.ExecuteTaskSynchronously(serviceProviderStartup.ConfigureServices(services));
                })
                ;

            return serviceProviderBuilder;
        }

        public static TServiceProviderBuilder UseServiceProviderStartup<TServiceProviderStartup, TServiceProviderBuilder>(this TServiceProviderBuilder serviceProviderBuilder,
            IServiceProvider serviceProviderStartupServiceProvider)
            where TServiceProviderStartup : IServiceProviderStartup
            where TServiceProviderBuilder :
                IHasConfigureServices<TServiceProviderBuilder>
        {
            var serviceProviderStartup = serviceProviderStartupServiceProvider.GetRequiredService<TServiceProviderStartup>();

            serviceProviderBuilder.UseServiceProviderStartup(serviceProviderStartup);

            return serviceProviderBuilder;
        }
    }
}