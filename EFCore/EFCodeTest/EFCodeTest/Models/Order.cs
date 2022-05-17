namespace EFCodeTest.Models;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<OrderLine>? Lines { get; set; }
}

public class OrderLine
{
    public int Id { get; set; }
    public string Name { get; set; }
}
