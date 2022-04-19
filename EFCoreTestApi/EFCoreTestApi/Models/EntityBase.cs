namespace EFCoreTestApi.Models;

public interface IEntityBase<TKey>
{
    TKey? Id { get; set; }
}

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public virtual TKey? Id { get; set; }
}

public interface IBusinessEntityBase<Tkey> : IEntityBase<Tkey>
{
}

public abstract class BusinessEntityBase<TKey> : EntityBase<TKey>
{
}