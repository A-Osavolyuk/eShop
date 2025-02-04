using eShop.Domain.Common.Api;
using eShop.Domain.Requests.Api.Auth;

namespace eShop.Domain.Interfaces.Client;

public interface IProfileService
{
    public ValueTask<Response> GetPhoneNumberAsync(string email);
    public ValueTask<Response> GetPersonalDataAsync(string email);
    public ValueTask<Response> ChangeUserNameAsync(ChangeUserNameRequest request);
    public ValueTask<Response> ChangePersonalDataAsync(ChangePersonalDataRequest request);
}