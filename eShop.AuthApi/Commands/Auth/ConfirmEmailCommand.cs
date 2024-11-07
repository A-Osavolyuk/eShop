using eShop.AuthApi.Services.Interfaces;
using eShop.Domain.Requests.Cart;

namespace eShop.AuthApi.Commands.Auth
{
    public record ConfirmEmailCommand(ConfirmEmailRequest Request) : IRequest<Result<ConfirmEmailResponse>>;

    public class ConfirmEmailCommandHandler(
        AppManager appManager,
        ILogger<ConfirmEmailCommandHandler> logger,
        IRequestClient<CreateCartRequest> cartRequestClient,
        IEmailSender emailSender) : IRequestHandler<ConfirmEmailCommand, Result<ConfirmEmailResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmEmailCommandHandler> logger = logger;
        private readonly IRequestClient<CreateCartRequest> requestClient = cartRequestClient;
        private readonly IEmailSender emailSender = emailSender;

        public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage =
                new ActionMessage("confirm email address of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to confirm email of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<ConfirmEmailResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                var token = Uri.UnescapeDataString(request.Request.Token);
                var confirmResult = await appManager.UserManager.ConfirmEmailAsync(user, token);

                if (!confirmResult.Succeeded)
                {
                    return logger.LogErrorWithException<ConfirmEmailResponse>(
                        new FailedOperationException(
                            $"Cannot confirm email address of user with email {request.Request.Email} " +
                            $"due to server error: {confirmResult.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation(
                    "Successfully confirmed email address of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                await emailSender.SendAccountRegisteredMessage(new AccountRegisteredMessage()
                {
                    To = request.Request.Email,
                    Subject = "Successful Account Registration",
                    UserName = user.UserName!
                });

                logger.LogInformation("Attempting to create cart for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var handler = requestClient.Create(new CreateCartRequest() { UserId = Guid.Parse(user.Id) });
                var response = await handler.GetResponse<ResponseDTO>();

                if (!response.Message.IsSucceeded)
                {
                    return logger.LogErrorWithException<ConfirmEmailResponse>(
                        new FailedRpcException(response.Message.ErrorMessage),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully created cart for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                return new(new ConfirmEmailResponse() { Message = "Your email address was successfully confirmed." });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmEmailResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}