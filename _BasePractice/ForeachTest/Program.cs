// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var root = new Recursive("root");
for (int i = 0; i < 5; i++)
{
    var child = new Recursive(root.Name + "." + i.ToString());
    for (int j = 0; j < 5; j++)
    {
        child.Children.Add(new Recursive(child.Name + "." + j.ToString()));
    }
    root.Children.Add(child);
}

var recursives = new List<Recursive>();
recursives.Add(root);

// 抛错 System.InvalidOperationException:“Collection was modified; enumeration operation may not execute.”
foreach (var item in recursives)
{
    GetRecursive(recursives, item);
}

foreach (var item in recursives)
{
    System.Console.WriteLine(item.Name);
}

void GetRecursive(List<Recursive> recursives, Recursive recursive)
{
    recursives.AddRange(recursive.Children);
}
