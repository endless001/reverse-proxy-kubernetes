namespace Kubernetes.Gateway.Protocol;

public interface IDispatchTarget
{
    public Task SendAsync(byte[] utf8Bytes, CancellationToken cancellationToken);
}