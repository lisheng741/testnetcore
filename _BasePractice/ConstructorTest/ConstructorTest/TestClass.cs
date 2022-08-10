using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorTest;

public class TestClassPrivate
{
    public string Str { get; set; }

    private TestClassPrivate() { }
}

public class TestClass0
{
    public string Str { get; set; }
}

public class TestClass1
{
    public string Str { get; set; }

    public TestClass1(string str)
    {
        Str = str;
    }
}

public class TestClass2
{
    public string Str { get; set; }
    public int? Int { get; set; }

    public TestClass2(string str, int i)
    {
        Str = str;
        Int = i;
    }
}

public class TestClass3
{
    public string Str { get; set; }
    public int? Int { get; set; }

    public TestClass3(string str , int i = 998, string str1 = "测试测试！")
    {
        Str = str + str1;
        Int = i;
    }
}
