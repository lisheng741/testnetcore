namespace ActionWebApiTest.Code;

public class TestService : IDisposable
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public void Show()
    {
        Console.WriteLine($"测试服务 Show: {Id}");
    }

    public void Dispose()
    {
        Console.WriteLine($"Dispose: {Id}");
    }
}
