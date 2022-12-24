namespace DisposeTest.Code;

public class Test1 : IDisposable
{
    private bool _disposed;

    public string Name { get; set; }

    public Test1(string name)
    {
        Name = name;
    }

    ~Test1()
    {
        Console.WriteLine($"Test1 {Name} finalize");
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;

            if (disposing)
            {
                Console.WriteLine($"Test1 {Name} dispose managed resources");
            }

            Console.WriteLine($"Test1 {Name} clean up unmanged resources");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        //GC.SuppressFinalize(this);
    }
}
