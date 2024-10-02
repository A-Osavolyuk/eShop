using eShop.Domain.Entities;

namespace eShop.Domain.Responses.Admin
{
    public record class FindUserResponse
    {
        public AccountData AccountData { get; set; } = null!;
        public PersonalData PersonalData { get; set; } = null!;
    }
}
