using ClassAndStructTest;
using System.Text.Json;

STest sTest = new STest();
sTest.Name = "12333";
Console.WriteLine(JsonSerializer.Serialize(sTest));

STest2 sTest2 = new STest2();
sTest2.Name = "12344";
Console.WriteLine(JsonSerializer.Serialize(sTest2));

StructTest structTest = new StructTest();
Console.WriteLine(JsonSerializer.Serialize(structTest));

(int i, string str) = (1, "123");
Console.WriteLine(i);
Console.WriteLine(JsonSerializer.Serialize((1, "123")));

// 匿名类型
var obj = new
{
    a = "123"
};

