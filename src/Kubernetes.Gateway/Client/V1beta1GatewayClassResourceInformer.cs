using k8s;
using Kubernetes.Gateway.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;

namespace Kubernetes.Gateway.Client;


public class V1beta1GatewayClassResourceInformer : ResourceInformer<V1beta1GatewayClass, V1beta1GatewayClassList>
{
    public V1beta1GatewayClassResourceInformer(IKubernetes client, IHostApplicationLifetime hostApplicationLifetime,
        ILogger logger) : base(client, hostApplicationLifetime, logger)
    {
    }

    protected override Task<HttpOperationResponse<V1beta1GatewayClassList>> RetrieveResourceListAsync(
        bool? watch = null, string resourceVersion = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}