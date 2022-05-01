namespace EFCodeTest.Models
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
