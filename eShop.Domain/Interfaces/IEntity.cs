namespace eShop.Domain.Interfaces;

public interface IEntity<TKey> : IAuditable
{
    public TKey Id { get; set; }
}