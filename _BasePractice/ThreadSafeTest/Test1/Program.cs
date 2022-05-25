
// see : https://www.jb51.net/article/232576.htm

Console.WriteLine(" --------------------------- ");

var tasks = new List<Task>();
for (int i = 0; i < 5; i++)
{
    tasks.Add(RunTask(i));
}

await Task.WhenAll(tasks);

Console.WriteLine(" --------------------------- ");

Console.ReadLine();

//async Task RunTask(int i)
//{
//    await Task.Run(() =>
//    {
//        Console.WriteLine($"begin {i} --- {Thread.CurrentThread.ManagedThreadId}");
//        Thread.Sleep(2000);
//        Console.WriteLine($"end {i} --- {Thread.CurrentThread.ManagedThreadId}");
//    });
//}

async Task RunTask(int i)
{
    await Task.Run(async () =>
    {
        Console.WriteLine($"begin {i} --- {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(2000);
        Console.WriteLine($"end {i} --- {Thread.CurrentThread.ManagedThreadId}");
    });
}
