namespace DisposeTest.Code;

public class Test2 : IDisposable
{
    private bool _disposed;

    public string Name { get; set; }

    public Test2(string name)
    {
        Name = name;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;

            if (disposing)
            {
                Console.WriteLine($"Test2 {Name} dispose managed resources");
            }

            Console.WriteLine($"Test2 {Name} clean up unmanged resources");
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
}
