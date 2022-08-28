using k8s.Models;
using Kubernetes.Gateway.Models;

namespace Kubernetes.Gateway.Caching;

public struct GatewayData
{
    public GatewayData(V1beta1Gateway gateway)
    {
        if (gateway is null)
        {
            throw new ArgumentNullException(nameof(gateway));
        }

        Spec = gateway.Spec;
        Metadata = gateway.Metadata;
    }
    
    public V1beta1GatewaySpec Spec { get; set; }
    public V1ObjectMeta Metadata { get; set; }
}