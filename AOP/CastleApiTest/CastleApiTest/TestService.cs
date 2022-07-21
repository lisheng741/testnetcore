using Castle.DynamicProxy;

namespace CastleApiTest;

public class TestService
{
    public TestService(ILogger<TestService> logger)
    {

    }

    public virtual void Show()
    {
        Console.WriteLine("测试方法！！！");
    }
}

public class TestServiceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine("之前");
        invocation.Proceed();
        Console.WriteLine("之后");
    }
}
