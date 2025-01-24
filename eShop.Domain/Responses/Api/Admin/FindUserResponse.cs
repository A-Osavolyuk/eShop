using eShop.Domain.Entities.Api.Auth;
using eShop.Domain.Types;

namespace eShop.Domain.Responses.Api.Admin;

public record class FindUserResponse
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalDataEntity PersonalDataEntity { get; set; } = null!;
    public PermissionsData PermissionsData {  get; set; } = null!; 
}