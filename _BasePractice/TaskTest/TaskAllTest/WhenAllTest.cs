namespace TaskAllTest;

internal class WhenAllTest
{
    // 正常走完
    //static async Task Main(string[] args)
    //{
    //    var tasks = new List<Task>();
    //    tasks.Add(Test1());
    //    tasks.Add(Test2());
    //    tasks.Add(Test3());

    //    await Task.WhenAll(tasks);
    //    Console.WriteLine("结束------");
    //}

    static async Task Main1(string[] args)
    {
        var tasks = new List<Task>();

        tasks.Add(Test1());
        tasks.Add(Test2());
        tasks.Add(Test3());

        var allTasks = Task.WhenAll(tasks);

        try
        {
            await allTasks;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"allTasks.Exception: {allTasks.Exception}");
        }

        foreach(var task in tasks)
        {
            if(task.Exception != null)
            {
                Console.WriteLine(task.Exception.InnerException.Message);
            }
        }

        Console.WriteLine("结束------");
    }

    static async Task Test1()
    {
        await Task.Delay(1000);
        Console.WriteLine("Test1 完成");
        throw new Exception("Test1 异常");
    }

    static async Task Test2()
    {
        await Task.Delay(2000);
        Console.WriteLine("Test2 完成");
        throw new Exception("Test2 异常");
    }

    static async Task Test3()
    {
        await Task.Delay(3000);
        Console.WriteLine("Test3 完成");
    }
}