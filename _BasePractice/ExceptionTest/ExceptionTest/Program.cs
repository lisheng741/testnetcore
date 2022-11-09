
Console.WriteLine(TestFunc());

var lst = TestListFunc();

lst.ForEach(x => Console.WriteLine(x));

string TestFunc()
{
    string result = "1234";
    try
    {
        return result;
    }
    finally
    {
        result = "567";
    }
}

List<string> TestListFunc()
{
    List<string> result = new List<string>();
    try
    {
        return result;
    }
    finally
    {
        result.Add("12345");
    }
}