using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterTest.Filters
{
    public class Test3ActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        internal const int _order = -1;

        public int Order => _order;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Test3ActionFilter before");
            await next();
            Console.WriteLine("Test3ActionFilter after");
        }
    }
}
