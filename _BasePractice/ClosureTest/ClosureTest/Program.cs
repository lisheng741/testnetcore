// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

List<Action> actions = new List<Action>();

for (int i = 0; i < 5; i++)
{
    actions.Add(() =>
    {
        var tmp = i;
        Console.WriteLine(tmp); // 输出都是5
    });
}

Console.WriteLine("----------");

for (int i = 0; i < 5; i++)
{
    var tmp = i;

    actions.Add(() =>
    {
        Console.WriteLine(tmp); // 输出5-9
    });
}

actions.ForEach(a => a.Invoke());
