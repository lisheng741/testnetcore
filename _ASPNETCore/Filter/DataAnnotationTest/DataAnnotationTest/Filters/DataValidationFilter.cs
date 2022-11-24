using DataAnnotationTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataAnnotationTest.Filters;

public class DataValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // 如果其他过滤器已经设置了结果，则跳过验证
        if (context.Result != null) return;

        // 如果验证通过，跳过后面的动作
        if (context.ModelState.IsValid) return;

        // 获取失败的验证信息列表
        var errors = context.ModelState
            .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
            .SelectMany(s => s.Value!.Errors.ToList())
            .Select(e => e.ErrorMessage)
            .ToArray();

        // 统一返回格式
        var result = new ApiResult()
        {
            Code = StatusCodes.Status400BadRequest,
            Msg = "数据验证不通过！",
            Data = errors
        };

        // 统一返回
        //var result = ApiResult.Status400BadRequest("数据验证不通过！", errors);

        // 设置结果
        context.Result = new BadRequestObjectResult(result);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
