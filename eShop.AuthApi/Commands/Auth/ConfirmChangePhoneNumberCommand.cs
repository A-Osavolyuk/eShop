using eShop.Domain.Requests.AuthApi.Auth;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.AuthApi.Commands.Auth;

internal sealed record ConfirmChangePhoneNumberCommand(ConfirmChangePhoneNumberRequest Request)
    : IRequest<Result<ConfirmChangePhoneNumberResponse>>;

internal sealed class ConfirmChangePhoneNumberCommandHandler(
    AppManager appManager,
    ITokenHandler tokenHandler)
    : IRequestHandler<ConfirmChangePhoneNumberCommand, Result<ConfirmChangePhoneNumberResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly ITokenHandler tokenHandler = tokenHandler;

    public async Task<Result<ConfirmChangePhoneNumberResponse>> Handle(ConfirmChangePhoneNumberCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var token = Uri.UnescapeDataString(request.Request.Token);
        var result =
            await appManager.UserManager.ChangePhoneNumberAsync(user, request.Request.PhoneNumber, token);

        if (!result.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot change phone number of user with email {request.Request.Email} " +
                $"due to server error: {result.Errors.First().Description}."));
        }

        user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
        var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
        var tokens = await tokenHandler.GenerateTokenAsync(user!, roles, permissions);

        return new(new ConfirmChangePhoneNumberResponse()
        {
            Message = "Your phone number was successfully changed.",
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
        });
    }
}