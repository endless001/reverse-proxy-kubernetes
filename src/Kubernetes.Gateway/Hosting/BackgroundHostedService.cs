using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kubernetes.Gateway.Hosting;

public abstract class BackgroundHostedService : IHostedService, IDisposable
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly CancellationTokenRegistration _hostApplicationStoppingRegistration;
    private readonly CancellationTokenSource _runCancellation = new();
    private readonly string _serviceTypeName;
    private bool _disposedValue;
    private Task _runTask;

    protected ILogger Logger { get; set; }

    public BackgroundHostedService(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger logger)
    {
        _hostApplicationLifetime =
            hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
        Logger = logger;

        _hostApplicationStoppingRegistration =
            hostApplicationLifetime.ApplicationStopping.Register(_runCancellation.Cancel);

        var serviceType = GetType();
        _serviceTypeName = serviceType.IsGenericType
            ? $"{serviceType.Name.Split('`').First()}<{string.Join(",", serviceType.GenericTypeArguments.Select(type => type.Name))}>"
            : serviceType.Name;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _runTask = Task.Run(CallRunAsync);
        return Task.CompletedTask;

        async Task CallRunAsync()
        {
            _runCancellation.Token.ThrowIfCancellationRequested();
            try
            {
                Logger?.LogInformation(
                    new EventId(1, "RunStarting"),
                    "Calling RunAsync for {BackgroundHostedService}",
                    _serviceTypeName);
                try
                {
                    await RunAsync(_runCancellation.Token).ConfigureAwait(true);
                }
                finally
                {
                    Logger?.LogInformation(
                        new EventId(2, "RunComplete"),
                        "RunAsync completed for {BackgroundHostedService}",
                        _serviceTypeName);
                }
            }
            catch (Exception e)
            {
                if (!_hostApplicationLifetime.ApplicationStopping.IsCancellationRequested)
                {
                    _hostApplicationLifetime.StopApplication();
                    Logger?.LogInformation(
                        new EventId(3, "RequestedStopApplication"),
                        "Called StopApplication for {BackgroundHostedService}",
                        _serviceTypeName);
                }

                throw;
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _runCancellation.Cancel();
            await _runTask.ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _runTask = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                try
                {
                    _runCancellation.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }

                try
                {
                    _hostApplicationStoppingRegistration.Dispose();
                }
                catch (ObjectDisposedException)
                {

                }
            }

            _disposedValue = true;
        }
    }

    public abstract Task RunAsync(CancellationToken cancellationToken);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}