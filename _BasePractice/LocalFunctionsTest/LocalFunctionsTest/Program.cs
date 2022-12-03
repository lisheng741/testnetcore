namespace LocalFunctionsTest;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var test = new TestClass();
        //test.Test();

        Console.WriteLine(test.Show());
        Console.WriteLine(test.ShowStatic()); 
    }
}

public class TestClass
{
    public string Show()
    {
        Console.WriteLine("show");
        string result;
        SetResult();
        return result;
        
        void SetResult()
        {
            result = "set result success"; // 捕获变量
        }
    }
    public string ShowStatic()
    {
        Console.WriteLine("ShowStatic");
        string result = "";
        SetResult();

        var test = () => result = "set result";
        test();

        return result;

        static void SetResult()
        {
            //result = "set result success"; // CS8421:静态本地函数不能包含对"result"的引用
        }
    }

    public void Test()
    {
        Console.WriteLine("Test function");
        Test();

        void Test()
        {
            Console.WriteLine("Test test");
        }
    }
}