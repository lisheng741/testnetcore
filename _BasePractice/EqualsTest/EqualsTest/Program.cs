
//string a = "aa", b = "aa";
string a = new String("aa"), b = new String("aa");
Console.WriteLine(a == b);
Console.WriteLine(a.Equals(b));

object oa = a;
object ob = b;
Console.WriteLine(oa == ob);
Console.WriteLine(oa.Equals(ob));

unsafe
{
    fixed(char* p = a)
    {
        Console.WriteLine("0x{0:x}", (int)p);
    }
    fixed (char* p = b)
    {
        Console.WriteLine("0x{0:x}", (int)p);
    }
}
