namespace Kubernetes.Gateway.Services;

public class QueueItem : IEquatable<QueueItem>
{
    public QueueItem(string change)
    {
        Change = change;
    }

    public string Change { get; }

    public override bool Equals(object obj)
    {
        return obj is QueueItem item && Equals(item);
    }

    public bool Equals(QueueItem other)
    {
        return Change.Equals(other.Change, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        return Change.GetHashCode();
    }

    public static bool operator ==(QueueItem left, QueueItem right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(QueueItem left, QueueItem right)
    {
        return !(left == right);
    }
}