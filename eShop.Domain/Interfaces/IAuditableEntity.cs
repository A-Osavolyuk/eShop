namespace eShop.Domain.Interfaces;

public interface IAuditableEntity<TKey> : IEntity<TKey>, IAuditable;