using Microsoft.AspNetCore.Http;

namespace Simple.Common;

public partial class ApiResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public ApiResult(int code = StatusCodes.Status200OK, string? message = AppResultMessage.Status200OK, object? data = null)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    public ApiResult(AppResult result)
    {
        Code = result.Code;
        Message = result.Message;
        Data = result.Data;
    }
}

public partial class ApiResult
{
    public static ApiResult Create(AppResult result)
    {
        return new ApiResult(result);
    }

    public static ApiResult Status200OK(string? message = AppResultMessage.Status200OK, object? data = null)
    {
        return new ApiResult(StatusCodes.Status200OK, message, data);
    }

    public static ApiResult Status400BadRequest(string? message = AppResultMessage.Status400BadRequest, object? data = null)
    {
        return new ApiResult(StatusCodes.Status400BadRequest, message, data);
    }

    public static ApiResult Status401Unauthorized(string? message = AppResultMessage.Status401Unauthorized, object? data = null)
    {
        return new ApiResult(StatusCodes.Status401Unauthorized, message, data);
    }

    public static ApiResult Status403Forbidden(string? message = AppResultMessage.Status403Forbidden, object? data = null)
    {
        return new ApiResult(StatusCodes.Status403Forbidden, message, data);
    }

    public static ApiResult Status404NotFound(string? message = AppResultMessage.Status404NotFound, object? data = null)
    {
        return new ApiResult(StatusCodes.Status404NotFound, message, data);
    }

    public static ApiResult Status500InternalServerError(string? message = AppResultMessage.Status500InternalServerError, object? data = null)
    {
        return new ApiResult(StatusCodes.Status500InternalServerError, message, data);
    }
}
