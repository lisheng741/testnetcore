namespace EFCoreTest.Models;

public abstract class EntityBase { }

public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct
{
    public TKey Id { get; set; }
}
