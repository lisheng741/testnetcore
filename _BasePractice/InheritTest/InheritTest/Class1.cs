using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritTest;

internal class Class1
{
    protected virtual void Call()
    {
        Console.WriteLine("这是class1的call");
    }

    public virtual void Show()
    {
        Call();
    }
}

internal class Class11 : Class1
{
    protected override void Call()
    {
        Console.WriteLine("这是新的call");
    }
}
