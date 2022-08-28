using Yarp.ReverseProxy.Configuration;

namespace Kubernetes.Gateway.Converters;

internal class YarpConfigContext
{
    public Dictionary<string, ClusterTransfer> ClusterTransfers { get; set; } = new();

    public List<RouteConfig> Routes { get; set; } = new();

    public List<ClusterConfig> BuildClusterConfig()
    {
        return ClusterTransfers.Values.Select(c => new ClusterConfig()
        {
            Destinations = c.Destinations,
            ClusterId = c.ClusterId,
            HealthCheck = c.HealthCheck,
            LoadBalancingPolicy = c.LoadBalancingPolicy,
            SessionAffinity = c.SessionAffinity,
            HttpClient = c.HttpClientConfig
        }).ToList();
    }
}