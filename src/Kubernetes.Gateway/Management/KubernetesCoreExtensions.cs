using k8s;
using Kubernetes.Gateway.Client;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class KubernetesCoreExtensions
{
    public static IServiceCollection AddKubernetesCore(this IServiceCollection services)
    {
        if (!services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IKubernetes)))
        {
            services = services.Configure<KubernetesClientOptions>(options =>
            {
                if (options.Configuration is null)
                {
                    options.Configuration = KubernetesClientConfiguration.BuildDefaultConfig();
                }
            });

            services = services.AddSingleton<IKubernetes>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KubernetesClientOptions>>().Value;

                return new k8s.Kubernetes(options.Configuration);
            });
        }

        return services;
    }
}