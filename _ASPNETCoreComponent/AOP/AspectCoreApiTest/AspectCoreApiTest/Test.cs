using AspectCore.DynamicProxy;

namespace AspectCoreApiTest;

public interface ITestService
{
    void Show();
}

public class TestService : ITestService
{
    
    public void Show()
    {
        Console.WriteLine("Showing");
    }
}

public class Test2Service
{
    public void Show()
    {
        Console.WriteLine("Test2 Showing");
    }
}

public class TestInterceptorAttribute : AbstractInterceptorAttribute
{
    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        Console.WriteLine("之前");
        await next.Invoke(context);
        Console.WriteLine("之后");
    }
}
