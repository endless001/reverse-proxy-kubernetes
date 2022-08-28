using k8s.Models;
using Kubernetes.Gateway;
using Kubernetes.Gateway.Client;
using Kubernetes.Gateway.ConfigProvider;
using Kubernetes.Gateway.Protocol;
using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class KubernetesReverseProxyServiceCollectionExtensions
{
    public static IReverseProxyBuilder AddKubernetesReverseProxy(this IServiceCollection services,
        IConfiguration config)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        // Add components from the kubernetes controller framework
        services.AddKubernetesControllerRuntime();

        // Add components implemented by this application
        services.Configure<YarpOptions>(config.GetSection("Yarp"));

        var provider = new KubernetesConfigProvider();
        services.AddSingleton<IProxyConfigProvider>(provider);
        services.AddSingleton<IUpdateConfig>(provider);

        // Register the necessary Kubernetes resource informers
        services.RegisterResourceInformer<V1Service, V1ServiceResourceInformer>();
        services.RegisterResourceInformer<V1Endpoints, V1EndpointsResourceInformer>();

        return services.AddReverseProxy();
    }

    public static IMvcBuilder AddKubernetesDispatchController(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(DispatchController).Assembly);
    }
}