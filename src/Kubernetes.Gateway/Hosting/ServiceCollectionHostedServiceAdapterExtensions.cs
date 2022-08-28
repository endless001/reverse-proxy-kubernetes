using Kubernetes.Gateway.Hosting;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionHostedServiceAdapterExtensions
{
    public static IServiceCollection RegisterHostedService<TService>(this IServiceCollection services)
        where TService : IHostedService
    {
        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(HostedServiceAdapter<TService>)))
        {
            services = services.AddHostedService<HostedServiceAdapter<TService>>();
        }

        return services;
    }
}