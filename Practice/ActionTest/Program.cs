// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.ReadLine();
Action actionMessage = HelloMessage;
actionMessage();

Console.ReadLine();
actionMessage = GoodByeMessage;
actionMessage();

Console.ReadLine();

void HelloMessage(){
    System.Console.WriteLine("Hello");
}

void GoodByeMessage(){
    System.Console.WriteLine("Good Bye");
}
