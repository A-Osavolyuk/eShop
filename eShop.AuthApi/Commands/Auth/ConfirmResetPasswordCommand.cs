using eShop.Domain.Requests.AuthApi.Auth;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.AuthApi.Commands.Auth;

internal sealed record ConfirmResetPasswordCommand(ConfirmResetPasswordRequest Request)
    : IRequest<Result<ConfirmResetPasswordResponse>>;

internal sealed class ConfirmResetPasswordCommandHandler(
    AppManager appManager,
    ILogger<ConfirmResetPasswordCommandHandler> logger)
    : IRequestHandler<ConfirmResetPasswordCommand, Result<ConfirmResetPasswordResponse>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<ConfirmResetPasswordResponse>> Handle(ConfirmResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var token = Uri.UnescapeDataString(request.Request.ResetToken);
        var resetResult =
            await appManager.UserManager.ResetPasswordAsync(user, token, request.Request.NewPassword);

        if (!resetResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot reset password for user with email {request.Request.Email} " +
                $"due to server error: {resetResult.Errors.First().Description}."));
        }

        return new(new ConfirmResetPasswordResponse()
        {
            Message = "Your password has been successfully reset."
        });
    }
}