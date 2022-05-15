public class Recursive
{
    public string Name { get; set; }
    public List<Recursive> Children { get; set; }

    public Recursive(string name)
    {
        Name = name;
        Children = new List<Recursive>();
    }
}