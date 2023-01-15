using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterTest.Filters
{
    public class Test1ActionFilter : IActionFilter, IOrderedFilter
    {
        // 无效
        internal const int _order = 1;

        public int Order => _order;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Test1ActionFilter OnActionExecuting");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Test1ActionFilter OnActionExecuting");
        }
    }
}
