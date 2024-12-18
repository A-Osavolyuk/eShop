using eShop.Domain.Entities.AuthApi;

namespace eShop.Domain.Responses.AuthApi.Admin;

public record class FindUserResponse
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalDataEntity PersonalDataEntity { get; set; } = null!;
    public PermissionsData PermissionsData {  get; set; } = null!; 
}