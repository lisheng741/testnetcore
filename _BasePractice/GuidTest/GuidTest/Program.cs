using GuidTest;
using Simple.Common.Helpers;
using System.Reflection;

for (int j = 0; j < 10; j++)
{
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"new Guid(\"{GuidHelper.Next()}\"),");
    }
    Console.WriteLine("");
    Console.WriteLine("-------");
    Console.WriteLine("");
}


// Guid 类型

var guidType = typeof(Guid);
var guidNullType = typeof(Guid?);
Console.WriteLine(typeof(Guid).FullName);
Console.WriteLine(typeof(Guid?).FullName);


// Guid 默认值

TestClass test = new TestClass();

if(test.Id == default)
{
    test.Id = Guid.NewGuid();
}

Guid? nullGuid = default;
Guid notNullGuid = default;



// 连续 Guid 测试

for (int i = 0; i < 10; i++)
{
    //Guid guid = GuidHelper.NextNew();
    //Console.WriteLine($"{i}：{guid}");
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
