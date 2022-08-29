using k8s;
using k8s.Models;
using Newtonsoft.Json;

namespace Kubernetes.Gateway.Models;

[KubernetesEntity(ApiVersion = KubeApiVersion, Group = KubeGroup, Kind = KubeKind, PluralName = "gatewayes")]
public class V1beta1Gateway : IKubernetesObject<V1ObjectMeta>, ISpec<V1beta1GatewaySpec>
{
    public const string KubeApiVersion = "v1beta1";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubeKind = "Gateway";

    [JsonProperty("apiVersion")] public string ApiVersion { get; set; }
    [JsonProperty("kind")] public string Kind { get; set; }
    [JsonProperty("metadata")] public V1ObjectMeta Metadata { get; set; }
    [JsonProperty("spec")] public V1beta1GatewaySpec Spec { get; set; }
}

public class V1beta1GatewaySpec
{
    [JsonProperty(PropertyName = "gatewayClassName")]
    public string GatewayClassName { get; set; }
}

public class V1beta1GatewayList : IKubernetesObject<V1ListMeta>,
    IKubernetesObject,
    IMetadata<V1ListMeta>,
    IItems<V1beta1Gateway>
{
    public string ApiVersion { get; set; }
    public string Kind { get; set; }
    public V1ListMeta Metadata { get; set; }
    public IList<V1beta1Gateway> Items { get; set; }
}
