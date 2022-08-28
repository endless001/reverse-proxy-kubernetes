namespace Kubernetes.Gateway.Protocol;

public interface IDispatcher
{
    Task AttachAsync(IDispatchTarget target, CancellationToken cancellationToken);
    void Detach(IDispatchTarget target);
    Task SendAsync(byte[] utf8Bytes, CancellationToken cancellationToken);
}