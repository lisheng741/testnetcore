using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultFilterTest.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultFilterTest.Filters
{
    public class UnifyResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var data = context.Result as ObjectResult;

            var result = new AppResult<object>("200", "成功！", data.Value);

            context.Result = new JsonResult(result);
        }
    }
}
