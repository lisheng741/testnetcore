using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStructTest;

public interface ITest
{
    string Name { get; set; }
}

public struct STest : ITest
{
    public string Name { get; set; }
}

public struct STest2
{
    public string Name;
}

public struct StructTest : ITest
{
    public StructTest()
    {
        Str = "123";
        Obj = new object();
        Name = "1233";
    }

    public string Str { get; set; } = "123";

    public object Obj { get; }

    public string Name { get; set; }
}
