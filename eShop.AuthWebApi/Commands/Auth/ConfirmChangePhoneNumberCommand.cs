namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmChangePhoneNumberCommand(ConfirmChangePhoneNumberRequest Request) : IRequest<Result<ConfirmChangePhoneNumberResponse>>;

    public class ConfirmChangePhoneNumberCommandHandler(
        AppManager appManager,
        ILogger<ConfirmChangePhoneNumberCommandHandler> logger,
        ITokenHandler tokenHandler) : IRequestHandler<ConfirmChangePhoneNumberCommand, Result<ConfirmChangePhoneNumberResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmChangePhoneNumberCommandHandler> logger = logger;
        private readonly ITokenHandler tokenHandler = tokenHandler;

        public async Task<Result<ConfirmChangePhoneNumberResponse>> Handle(ConfirmChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("confirm change phone number of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to confirm change phone number of user with email {email}. Request ID {requestId}.",
                    request.Request.Email, request.Request.RequestId);
                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(new NotFoundUserByEmailException(request.Request.Email),
                        actionMessage, request.Request.RequestId);
                }

                var token = Uri.UnescapeDataString(request.Request.Token);
                var result = await appManager.UserManager.ChangePhoneNumberAsync(user, request.Request.PhoneNumber, token);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(new NotChangedPhoneNumberException(), actionMessage, request.Request.RequestId);
                }

                user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(new NotFoundUserByEmailException(request.Request.Email),
                        actionMessage, request.Request.RequestId);
                }

                var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                var tokens = await tokenHandler.GenerateTokenAsync(user!, roles, permissions);

                logger.LogInformation("Successfully changed phone number of user with email {email}. Request ID {requestId}",
                        request.Request.Email, request.Request.RequestId);
                return new(new ConfirmChangePhoneNumberResponse()
                {
                    Message = "Your phone number was successfully changed.",
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
