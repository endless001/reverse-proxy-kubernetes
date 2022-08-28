using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace Kubernetes.Gateway.Protocol;

public class Dispatcher : IDispatcher
{
    private readonly ILogger<Dispatcher> _logger;
    private readonly object _targetsSync = new();
    private ImmutableList<IDispatchTarget> _targets = ImmutableList<IDispatchTarget>.Empty;
    private byte[] _lastMessage;

    public Dispatcher(ILogger<Dispatcher> logger)
    {
        _logger = logger;
    }

    public async Task AttachAsync(IDispatchTarget target, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Attaching {DispatchTarget}", target?.ToString());

        lock (_targetsSync)
        {
            _targets = _targets.Add(target);
        }

        if (_lastMessage is not null)
        {
            await target.SendAsync(_lastMessage, cancellationToken).ConfigureAwait(false);
        }
    }

    public void Detach(IDispatchTarget target)
    {
        _logger.LogDebug("Detaching {DispatchTarget}", target?.ToString());
        lock (_targetsSync)
        {
            _targets = _targets.Remove(target);
        }
    }

    public async Task SendAsync(byte[] utf8Bytes, CancellationToken cancellationToken)
    {
        _lastMessage = utf8Bytes;
        foreach (var target in _targets)
        {
            await target.SendAsync(utf8Bytes, cancellationToken).ConfigureAwait(false);
        }
    }
}