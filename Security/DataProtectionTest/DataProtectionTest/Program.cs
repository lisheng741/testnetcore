using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;

namespace DataProtectionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();

            var instance = ActivatorUtilities.CreateInstance<MyClass>(services);
            instance.RunSample();

            Console.WriteLine("Hello, World!");
        }
    }

    public class MyClass
    {
        IDataProtector _protector;

        public MyClass(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("test");
        }

        public void RunSample()
        {
            Console.WriteLine("Enter input:");
            string input = Console.ReadLine() ?? "";

            var protectedInput = _protector.Protect(input);
            Console.WriteLine($"protect: {protectedInput}");
            Console.WriteLine($"unprotect: {_protector.Unprotect(protectedInput)}");
        }
    }
}