using NLog;
using NLog.Web;
using WorkerServiceTest;

Console.WriteLine($"Program: {Thread.CurrentThread.ManagedThreadId}");

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Æô¶¯");

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<Worker2>();
    })
    .UseNLog()
    .Build();

await host.RunAsync();
