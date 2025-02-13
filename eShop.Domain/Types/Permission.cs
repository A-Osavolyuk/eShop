namespace eShop.Domain.Types;

public class Permission : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}