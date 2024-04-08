namespace eShop.Domain.Entities
{
    public class SupplierEntity
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
