using Microsoft.Extensions.Hosting;

namespace Kubernetes.Gateway.Hosting;

public class HostedServiceAdapter<TService> : IHostedService
    where TService : IHostedService
{
    private readonly TService _service;
    public HostedServiceAdapter(TService service) => _service = service;
    public Task StartAsync(CancellationToken cancellationToken) => _service.StartAsync(cancellationToken);
    public Task StopAsync(CancellationToken cancellationToken) => _service.StopAsync(cancellationToken);
}