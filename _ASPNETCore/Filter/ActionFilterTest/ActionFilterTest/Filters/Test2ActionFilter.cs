using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterTest.Filters
{
    public class Test2ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Test2ActionFilter 4");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Test2ActionFilter 3");
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Test2ActionFilter 1");

            await base.OnActionExecutionAsync(context, next);

            Console.WriteLine("Test2ActionFilter 2");
        }
    }
}
