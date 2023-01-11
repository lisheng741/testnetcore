using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterTest.Filters
{
    public class Test3ActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Test3ActionFilter before");
            await next();
            Console.WriteLine("Test3ActionFilter after");
        }
    }
}
