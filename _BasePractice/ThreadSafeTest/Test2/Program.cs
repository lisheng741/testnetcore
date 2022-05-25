
var _lock = new object();
var list = new List<int>();
var tasks = new List<Task>();
for (int i = 0; i < 200; i++)
{
    var k = i;
    tasks.Add(Task.Run(async () => { 
        lock(_lock)
        {
            list.Add(k);
        }
        await Task.Delay(1);
    }));
}
await Task.WhenAll(tasks);

list.ForEach(i => Console.Write($"{i},"));

Console.ReadLine();
