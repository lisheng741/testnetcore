using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataAnnotationTest.Filters;

public class DataValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var modelState = context.ModelState;

        if (modelState.IsValid) return;

        var values = modelState.Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
                               .SelectMany(s => s.Value!.Errors.ToList())
                               .Select(e => e.ErrorMessage)
                               .ToList();

        var data = string.Join('\n', values);

        Console.WriteLine(data);

        var result = new
        {
            Code = 400,
            Msg = data,
            data = values
        };

        context.Result = new BadRequestObjectResult(result);
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
