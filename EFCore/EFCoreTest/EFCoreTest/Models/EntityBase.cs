using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Models;

public abstract class EntityBase
{
    public virtual void ConfigureEntity(ModelBuilder builder)
    {
    }
}

public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct
{
    public TKey Id { get; set; }
}

public abstract class BusinessEntityBase<TKey> : EntityBase<TKey>, ISoftDelete, ICreatedInfo, IUpdatedInfo
    where TKey : struct
{
    public bool IsDeleted { get; set; }
    public DateTime? CreatedTime { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public Guid? UpdatedUserId { get; set; }
}

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}

public interface ITenant
{
    public int TenantId { get; set; }
}

public interface ICreatedInfo
{
    public DateTime? CreatedTime { get; set; }
    public Guid? CreatedUserId { get; set; }
}

public interface IUpdatedInfo
{
    public DateTime? UpdatedTime { get; set; }
    public Guid? UpdatedUserId { get; set; }
}
