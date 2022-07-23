using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Models;

public class TestDelete : EntityBase<int>, ISoftDelete
{
    public string? Name { get; set; }
    public bool IsDeleted { get; set; } = false;

    public override void ConfigureEntity(ModelBuilder builder)
    {
        base.ConfigureEntity(builder);
        builder.Entity<TestDelete>().Property(nameof(Name)).HasComment("名称");
    }
}
