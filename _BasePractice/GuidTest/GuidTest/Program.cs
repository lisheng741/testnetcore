using GuidTest;
using Simple.Common.Guids;
using System.Reflection;

for (int i = 0; i < 10; i++)
{
    Guid guid = GuidHelper.NextNew();
    Console.WriteLine($"{i}：{guid}");
}

Console.WriteLine("---------------------");

GuidHelper.SequentialGuidType = SequentialGuidType.AsString;

byte[] bytes = new byte[16];

bytes[0] = (byte)1;

Console.WriteLine(new Guid(bytes));

Console.WriteLine(new Guid(bytes));

Console.WriteLine("---------------------");

List<Guid> list = new List<Guid>();

for(int i = 0; i < 10; i++)
{
    Guid guid = GuidHelper.Next();
    list.Add(guid);
    Console.WriteLine($"{i}：{guid}");
}

Console.WriteLine("---------------------");

list.Sort();

foreach (Guid id in list)
{
    Console.WriteLine(id);
}

Console.WriteLine("---------------------");

Parallel.For(0, 10, i => Console.WriteLine($"{i}：{GuidHelper.Next()}"));
