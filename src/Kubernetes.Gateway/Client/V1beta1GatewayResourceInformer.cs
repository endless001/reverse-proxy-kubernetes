using k8s;
using k8s.Models;
using Kubernetes.Gateway.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;

namespace Kubernetes.Gateway.Client;

public class V1beta1GatewayResourceInformer : ResourceInformer<V1beta1Gateway, V1beta1GatewayList>
{
    public V1beta1GatewayResourceInformer(IKubernetes client, IHostApplicationLifetime hostApplicationLifetime,
        ILogger logger) : base(client, hostApplicationLifetime, logger)
    {
    }

    protected override Task<HttpOperationResponse<V1beta1GatewayList>> RetrieveResourceListAsync(bool? watch = null,
        string resourceVersion = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
