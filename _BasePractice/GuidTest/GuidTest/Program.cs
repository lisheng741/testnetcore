using GuidTest;
using Simple.Common.Guids;
using System.Reflection;

foreach(PropertyInfo info in typeof(TestClass).GetProperties())
{
    Console.WriteLine(info);
    if(info.PropertyType == typeof(System.Guid))
    {
        Console.WriteLine(info.PropertyType.FullName);
    }
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
