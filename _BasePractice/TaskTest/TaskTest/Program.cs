
//Parallel.For(0, 5, n =>
//{
//    Task.Run(async() =>
//    {
//        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
//        await Task.Delay(500);
//        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
//    });
//});

//Console.WriteLine("----------------------------");

//parallel.for(0, 5, n =>
//{
//    task.run(() =>
//    {
//        console.writeline($"任务：{n} 线程：{thread.currentthread.managedthreadid}");
//        task.delay(500).wait();
//        console.writeline($"任务：{n} 线程：{thread.currentthread.managedthreadid}");
//    });
//});



//Parallel.For(0, 5, n =>
//{
//    Task.Run(() =>
//    {
//        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
//        Task.Delay(500).ConfigureAwait(true);
//        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
//    });
//});

//Console.WriteLine("----------------------------");

Parallel.For(0, 5, n =>
{
    Task.Run(() =>
    {
        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
        Task.Delay(500).ConfigureAwait(false);
        Console.WriteLine($"任务：{n} 线程：{Thread.CurrentThread.ManagedThreadId}");
    });
});

await Task.Delay(5000);

Console.WriteLine("----------------------------");

Console.ReadLine();
