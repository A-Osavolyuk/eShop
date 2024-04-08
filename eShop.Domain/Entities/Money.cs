using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; } = Currency.Dollar;
    }
}
