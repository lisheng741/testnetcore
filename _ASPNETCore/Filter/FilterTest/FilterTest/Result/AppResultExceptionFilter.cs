using Microsoft.AspNetCore.Mvc.Filters;

namespace Simple.Common.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AppResultExceptionFilter : Attribute, IAsyncExceptionFilter, IExceptionFilter, IOrderedFilter
{
    public int Order { get; set; } = -6000;

    public virtual void OnException(ExceptionContext context)
    {
        // 如果其他过滤器已经设置了结果，则直接返回
        if (context.Result != null) return;

        // 如果其他过滤器已经处理了异常，则直接返回
        if(context.ExceptionHandled) return;

        // 
        if (!(context.Exception is AppResultException resultException)) return;

        context.ExceptionHandled = true;
    }

    public virtual Task OnExceptionAsync(ExceptionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        OnException(context);
        return Task.CompletedTask;
    }
}
