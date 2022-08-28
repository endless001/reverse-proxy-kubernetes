namespace Kubernetes.Gateway.Queues;

public interface IWorkQueue<TItem> : IDisposable
{
    void Add(TItem item);
    int Len();
    Task<(TItem item, bool shutdown)> GetAsync(CancellationToken cancellationToken);
    void Done(TItem item);
    void ShutDown();
    bool ShuttingDown();
}