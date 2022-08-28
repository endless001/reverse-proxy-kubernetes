namespace Kubernetes.Gateway.Client;

public interface IResourceInformerRegistration : IDisposable
{
    Task ReadyAsync(CancellationToken cancellationToken);
}