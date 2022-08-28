using k8s;
using k8s.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;

namespace Kubernetes.Gateway.Client;

internal class V1EndpointsResourceInformer : ResourceInformer<V1Endpoints, V1EndpointsList>
{
    public V1EndpointsResourceInformer(
        IKubernetes client,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<V1EndpointsResourceInformer> logger)
        : base(client, hostApplicationLifetime, logger)
    {
    }

    protected override Task<HttpOperationResponse<V1EndpointsList>> RetrieveResourceListAsync(bool? watch = null,
        string resourceVersion = null,
        CancellationToken cancellationToken = default)
    {
        return Client.ListEndpointsForAllNamespacesWithHttpMessagesAsync(watch: watch, resourceVersion: resourceVersion,
            cancellationToken: cancellationToken);
    }
}