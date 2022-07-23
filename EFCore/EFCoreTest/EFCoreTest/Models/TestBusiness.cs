using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Models;

public class TestBusiness : BusinessEntityBase<int>
{
    public string? Name { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        base.ConfigureEntity(builder);
        builder.Entity<TestBusiness>().Property(nameof(Name)).HasComment("名称");
        builder.Entity<TestBusiness>().Property(nameof(IsDeleted)).HasComment("软删");
        builder.Entity<TestBusiness>().Property(nameof(CreatedTime)).HasComment("创建时间");
        builder.Entity<TestBusiness>().Property(nameof(UpdatedTime)).HasComment("最后更新时间");
    }
}
