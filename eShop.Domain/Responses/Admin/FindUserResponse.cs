using eShop.Domain.Entities.Admin;

namespace eShop.Domain.Responses.Admin
{
    public record class FindUserResponse
    {
        public AccountData AccountData { get; set; } = null!;
        public PersonalDataEntity PersonalDataEntity { get; set; } = null!;
        public PermissionsData PermissionsData {  get; set; } = null!; 
    }
}
