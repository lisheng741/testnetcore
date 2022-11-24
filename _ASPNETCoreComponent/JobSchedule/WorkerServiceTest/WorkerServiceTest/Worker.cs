namespace WorkerServiceTest;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        Console.WriteLine($"Worker: {Thread.CurrentThread.ManagedThreadId}");
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Run1());
            tasks.Add(Run2());

            await Task.WhenAll(tasks);

            Console.WriteLine($"0: {Thread.CurrentThread.ManagedThreadId}");
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task Run1()
    {
        Console.WriteLine($"Run1 before: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(1000);
        Console.WriteLine($"Run1 after: {Thread.CurrentThread.ManagedThreadId}");
    }

    private async Task Run2()
    {
        Console.WriteLine($"Run2 before: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(1000);
        Console.WriteLine($"Run2 after: {Thread.CurrentThread.ManagedThreadId}");
    }
}