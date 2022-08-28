using Yarp.ReverseProxy.Configuration;

namespace Kubernetes.Gateway.ConfigProvider;

public interface IUpdateConfig
{
    Task UpdateAsync(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters, CancellationToken cancellationToken);
}