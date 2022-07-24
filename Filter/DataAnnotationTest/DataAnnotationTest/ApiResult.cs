namespace DataAnnotationTest;

public class ApiResult
{
    public int Code { get; set; }
    public string? Msg { get; set; }
    public object? Data { get; set; }

    public static ApiResult Status200OK()
        => new ApiResult() { Code = StatusCodes.Status200OK };

    public static ApiResult Status400BadRequest(string? msg)
        => Status400BadRequest(msg, null);

    public static ApiResult Status400BadRequest(string? msg, object? data)
        => new ApiResult() { Code = StatusCodes.Status400BadRequest, Msg = msg, Data = data };
}
