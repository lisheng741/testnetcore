using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionLinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>() { "1","2","3","3333"};

            Expression<Func<string, bool>> expr = s => s.Contains("3");

            var result = list.Where(expr.Compile());

            foreach(var s in result)
            {
                Console.WriteLine(s);
            }
        }
    }
}
