namespace LifecycleTest.Code;

public class SingletonTest
{
	private readonly TransientTest _transient;

    public Guid Guid { get; set; } = Guid.NewGuid();

    public SingletonTest(TransientTest transient)
	{
		_transient = transient;
	}

	public void Show()
    {
        Console.WriteLine(Guid);
        //Console.WriteLine(scope.Guid);
        Console.WriteLine(_transient.Guid);
    }
}

public class ScopeTest
{
	public Guid Guid { get; set; } = Guid.NewGuid();

	public ScopeTest(TransientTest transient)
	{
		Console.WriteLine(transient.Guid);
	}
}

public class TransientTest : IDisposable
{
	private bool _disposed;
    public Guid Guid { get; set; } = Guid.NewGuid();

    public TransientTest()
	{

	}

	public void Dispose()
	{
		Console.WriteLine($"{Guid} is disposed!!!!!!");

		Dispose(true);

        GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_disposed) return;

		if (disposing)
		{

		}

		_disposed = true;
	}
}
