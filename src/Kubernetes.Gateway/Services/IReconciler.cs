namespace Kubernetes.Gateway.Services;

public interface IReconciler
{
    Task ProcessAsync(CancellationToken cancellationToken);
}