namespace EFCoreTest.Models;

public interface ITenant
{
    public int TenantId { get; set; }
}

public class TestTenant : EntityBase<int>, ITenant, ISoftDelete
{
    public string? Name { get; set; }
    public int TenantId { get; set; }
    public bool IsDeleted { get; set; } = false;
}
