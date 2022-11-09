
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var read = Console.ReadLine();

        ReadAndShow(read);
    }


    public static string ReadAndShow(string? read)
    {
        if (read == "a")
        {
            Console.WriteLine("aaa");
            return "aaa";
        }
        else
        {
            Console.WriteLine("eeee");
            return "eeee";
        }
    }

    public static bool BoolTest(bool isTrue)
    {
        return isTrue;
    }
}