namespace TaskAllTest;

//internal class TaskAndAwait
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//        int[] list = { 1, 2, 3 };
//        var tasks = list.Select(x => ReturnTask(x));

//        await Task.WhenAll(tasks);
//    }

//    static async Task ReturnTask(int x)
//    {
//        Console.WriteLine($"ReturnTask {x}: {Thread.CurrentThread.ManagedThreadId}");
//        await AwaitTask(x);
//    }

//    static async Task AwaitTask(int x)
//    {
//        await Task.Delay(500);
//        Console.WriteLine($"AwaitTask {x}: {Thread.CurrentThread.ManagedThreadId}");
//    }
//}

//internal class TaskAndAwait
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//        int[] list = { 1, 2, 3 };
//        var tasks = list.Select(x => ReturnTask(x));

//        await Task.WhenAll(tasks);
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//    }

//    static async Task ReturnTask(int x)
//    {
//        Console.WriteLine($"- ReturnTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        await AwaitTask(x);
//        Console.WriteLine($"- ReturnTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//    }

//    static async Task AwaitTask(int x)
//    {
//        Console.WriteLine($"AwaitTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        await Task.Delay(500);
//        Console.WriteLine($"AwaitTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//    }
//}

//////////////////////////////////////////

//internal class TaskAndAwait
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//        int[] list = { 1, 2, 3 };
//        var tasks = list.Select(x => ReturnTask(x));

//        await Task.WhenAll(tasks);
//    }

//    static Task ReturnTask(int x)
//    {
//        Console.WriteLine($"ReturnTask {x}: {Thread.CurrentThread.ManagedThreadId}");
//        return AwaitTask(x);
//    }

//    static async Task AwaitTask(int x)
//    {
//        await Task.Delay(500);
//        Console.WriteLine($"AwaitTask {x}: {Thread.CurrentThread.ManagedThreadId}");
//    }
//}

//internal class TaskAndAwait
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//        int[] list = { 1, 2, 3 };
//        var tasks = list.Select(x => ReturnTask(x));

//        await Task.WhenAll(tasks);
//    }

//    static Task ReturnTask(int x)
//    {
//        Console.WriteLine($"- ReturnTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        var task = AwaitTask(x);
//        Console.WriteLine($"- ReturnTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//        return task;
//    }

//    static async Task AwaitTask(int x)
//    {
//        Console.WriteLine($"AwaitTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        await Task.Delay(500);
//        Console.WriteLine($"AwaitTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//    }
//}

////////////////////////////////////////////
///

//internal class TaskAndAwait
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//        int[] list = { 1, 2, 3 };
//        var tasks = list.Select(x => ReturnTask(x));

//        await Task.WhenAll(tasks);
//        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
//    }

//    static async Task ReturnTask(int x)
//    {
//        Console.WriteLine($"- ReturnTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        await MidTask(x);
//        Console.WriteLine($"- ReturnTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//    }

//    static Task MidTask(int x)
//    {
//        Console.WriteLine($"MidTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        var task = AwaitTask(x);
//        Console.WriteLine($"MidTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//        return task;
//    }

//    static async Task AwaitTask(int x)
//    {
//        Console.WriteLine($"* AwaitTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
//        await Task.Delay(500);
//        Console.WriteLine($"* AwaitTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
//    }
//}

internal class TaskAndAwait
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
        int[] list = { 1, 2, 3 };
        var tasks = list.Select(x => ReturnTask(x));

        await Task.WhenAll(tasks);
        Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");
    }

    static async Task ReturnTask(int x)
    {
        Console.WriteLine($"- ReturnTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
        await MidTask(x);
        Console.WriteLine($"- ReturnTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
    }

    static async Task MidTask(int x)
    {
        Console.WriteLine($"MidTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
        await AwaitTask(x);
        Console.WriteLine($"MidTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
    }

    static async Task AwaitTask(int x)
    {
        Console.WriteLine($"* AwaitTask {x} before: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(500);
        Console.WriteLine($"* AwaitTask {x} after: {Thread.CurrentThread.ManagedThreadId}");
    }
}
