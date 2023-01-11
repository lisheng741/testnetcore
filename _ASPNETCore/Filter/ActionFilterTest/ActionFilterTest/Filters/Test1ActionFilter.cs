using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterTest.Filters
{
    public class Test1ActionFilter : IActionFilter
    {
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
