using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceTest;

public class Worker2 : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"2: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
