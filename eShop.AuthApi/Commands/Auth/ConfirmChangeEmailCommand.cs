using eShop.Domain.Requests.Auth;
using eShop.Domain.Responses.Auth;

namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ConfirmChangeEmailCommand(ConfirmChangeEmailRequest Request)
        : IRequest<Result<ConfirmChangeEmailResponse>>;

    internal sealed class ConfirmChangeEmailCommandHandler(
        AppManager appManager,
        ILogger<ConfirmChangeEmailCommandHandler> logger)
        : IRequestHandler<ConfirmChangeEmailCommand, Result<ConfirmChangeEmailResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmChangeEmailCommandHandler> logger = logger;

        public async Task<Result<ConfirmChangeEmailResponse>> Handle(ConfirmChangeEmailCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("confirm change email address of user with current email {0}",
                request.Request.CurrentEmail);
            try
            {
                logger.LogInformation(
                    "Attempting to change email address of user with email {email}. Request ID {requestId}",
                    request.Request.CurrentEmail, request.Request.RequestId);
                var user = await appManager.UserManager.FindByEmailAsync(request.Request.CurrentEmail);

                if (user is null)
                {
                    return logger.LogInformationWithException<ConfirmChangeEmailResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.CurrentEmail}."),
                        actionMessage, request.Request.RequestId);
                }

                var token = Uri.UnescapeDataString(request.Request.Token);
                var result = await appManager.UserManager.ChangeEmailAsync(user, request.Request.NewEmail, token);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ConfirmChangeEmailResponse>(
                        new FailedOperationException(
                            $"Cannot change email address of user with email {request.Request.CurrentEmail} " +
                            $"due to server error: {result.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation(
                    "Successfully changed email address from {oldEmail} to {newEmail}. Request ID {requestId}",
                    request.Request.CurrentEmail, request.Request.NewEmail, request.Request.RequestId);

                return new(new ConfirmChangeEmailResponse()
                {
                    Message = "Your email address was successfully changed."
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmChangeEmailResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}