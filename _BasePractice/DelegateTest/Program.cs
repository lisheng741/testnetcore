
namespace DelegateTest;

public class Program
{
    delegate void DisplayMessage();

    // See https://aka.ms/new-console-template for more information
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        System.Console.WriteLine(args[0]);

        Console.ReadLine();
        DisplayMessage displayMessage = DisplayHello;
        displayMessage();

        Console.ReadLine();
        displayMessage = DisplayGooBye;
        displayMessage();

        void DisplayHello()
        {
            System.Console.WriteLine("Hello");
        }

        void DisplayGooBye()
        {
            System.Console.WriteLine("Good Bye");
        }
    }
}
