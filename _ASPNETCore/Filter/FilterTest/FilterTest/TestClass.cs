using Simple.Common;
using Simple.Common.Filters;

namespace FilterTest
{
    [AppResultExceptionFilter]
    public class TestClass
    {
        public string Str { get; set; }

        public TestClass()
        {
            Str = "???测试……";
        }

        public string Show()
        {
            throw new AppResultException();
            Console.WriteLine(Str);
            return Str;
        }
    }
}
