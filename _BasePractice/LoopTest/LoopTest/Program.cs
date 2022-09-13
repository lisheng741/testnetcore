// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Test? test = null;

foreach(var s in test?.Strings)
{
    Console.WriteLine(s);
}

class Test
{
    public List<string> Strings { get; set; } = new List<string>();
}

