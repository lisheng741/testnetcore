

string s = "2022061";

if (s.Length != 6) throw new Exception();

Console.WriteLine(s.Substring(0, 4));

Console.WriteLine(s.Substring(4, 2));

Console.ReadLine();
