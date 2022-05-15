// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.ReadLine();
Action actionMessage = HelloMessage;
actionMessage();

while (true)
{
    string? readLine = Console.ReadLine();
    if (string.IsNullOrEmpty(readLine))
    {
        break;
    }
    Action<string> copyMessage = CopyMessage;
    copyMessage(readLine);
    copyMessage.Invoke(readLine);
}

Console.ReadLine();
actionMessage = GoodByeMessage;
// actionMessage();
actionMessage.Invoke();

Console.ReadLine();

void HelloMessage()
{
    System.Console.WriteLine("Hello");
}

void GoodByeMessage()
{
    System.Console.WriteLine("Good Bye");
}

void CopyMessage(string msg)
{
    System.Console.WriteLine(msg);
}
