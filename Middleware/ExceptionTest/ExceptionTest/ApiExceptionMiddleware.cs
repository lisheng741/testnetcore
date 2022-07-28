using Microsoft.AspNetCore.Http.Features;
using Simple.Common.Helpers;
using SimpleApp.Common;
using System.Runtime.ExceptionServices;
using System.Text.Json;

namespace ExceptionTest;

public static class MiddlewareBuilderExtensions
{
    public static IApplicationBuilder UseApiException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ApiExceptionMiddleware>();
    }
}

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ExceptionDispatchInfo edi;
        try
        {
            await _next(context);
            return;
        }
        catch (Exception ex)
        {
            // 捕获异常，但不在 catch 块中继续处理，因为这样不利于堆栈的使用
            edi = ExceptionDispatchInfo.Capture(ex);
        }

        await HandleExceptionAsync(context, edi);
    }

    private async Task HandleExceptionAsync(HttpContext context, ExceptionDispatchInfo edi)
    {
        // 如果已经开始响应客户端，则该异常将无法处理
        if (context.Response.HasStarted)
        {
            _logger.LogError(edi.SourceException, "HTTP响应已经开始，无法处理该异常");
            edi.Throw();
            return;
        }

        // 日志记录
        _logger.LogError(edi.SourceException, "全局异常拦截");

        // 清空 Response，重设终结点（Endpoint）
        context.Response.Clear();
        context.SetEndpoint(endpoint: null);
        var routeValuesFeature = context.Features.Get<IRouteValuesFeature>();
        if (routeValuesFeature != null)
        {
            routeValuesFeature.RouteValues = null!;
        }

        // 响应头处理
        context.Response.Headers.CacheControl = "no-cache,no-store";
        context.Response.Headers.Pragma = "no-cache";
        context.Response.Headers.Expires = "-1";
        context.Response.Headers.ETag = default;

        // 响应
        var result = ApiResult.Status500InternalServerError("系统异常，请联系管理员");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}
