namespace MvcTest.Models;

public class InputModel
{
    public string Id { get; set; }
    public string Show { get; set; }
}

public class TestViewModel
{
    public string Name { get; set; }
    public string[] Items { get; set; }
    public TestViewModel()
    {
        Items = new string[] { "1aa", "bbb", "3cc3cc" };
    }
}
