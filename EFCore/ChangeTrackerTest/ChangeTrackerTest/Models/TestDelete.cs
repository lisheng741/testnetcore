namespace EFCoreTest.Models;

public class TestDelete : EntityBase<int>, ISoftDelete
{
    public string? Name { get; set; }
    public bool IsDeleted { get; set; } = false;
}
