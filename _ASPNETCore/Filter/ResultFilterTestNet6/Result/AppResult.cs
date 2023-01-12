using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultFilterTest.Result
{
    public class AppResult<T>
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public AppResult(string code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
