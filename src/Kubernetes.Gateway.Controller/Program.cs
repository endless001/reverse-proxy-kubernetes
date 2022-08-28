using Microsoft.Rest;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Kubernetes.Gateway.Controller;

public static class Program
{
    public static void Main(string[] args)
    {
        using var serilog = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

        ServiceClientTracing.IsEnabled = true;

        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => { config.AddJsonFile("/app/config/yarp.json", optional: true); })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSerilog(serilog, dispose: false);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).Build().Run();
    }
}
