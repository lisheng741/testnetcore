using InternalTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalTest
{
    internal class Namespace2
    {
        Namespace1 namespace1 = new Namespace1();
    }
}

namespace InternalTest2
{
    internal class Namespace2Class
    {
        Namespace2 namespace2 = new Namespace2();
        Namespace1 namespace1 = new Namespace1();
    }
}
