namespace AutoMapperTest.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }

    public User()
    {

    }

    public User(Guid id, string name, int? age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}
