namespace SemaphoreSlimTest;

public class Test
{
    private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

    public async Task Show(string name)
    {
        _lock.Wait();

        try
        {
            System.Console.WriteLine($"Show start...{name}");
            await Task.Delay(3000);
            System.Console.WriteLine($"Show end...{name}");
        }
        finally
        {
            _lock.Release();
        }
    }
}
