namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ConfirmEmailCommand(ConfirmEmailRequest Request) : IRequest<Result<ConfirmEmailResponse>>;

    internal sealed class ConfirmEmailCommandHandler(
        AppManager appManager,
        IRequestClient<CreateCartRequest> cartRequestClient,
        IEmailSender emailSender) : IRequestHandler<ConfirmEmailCommand, Result<ConfirmEmailResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly IRequestClient<CreateCartRequest> requestClient = cartRequestClient;
        private readonly IEmailSender emailSender = emailSender;

        public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

            if (user is null)
            {
                return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
            }

            var token = Uri.UnescapeDataString(request.Request.Token);
            var confirmResult = await appManager.UserManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
            {
                return new(new FailedOperationException(
                    $"Cannot confirm email address of user with email {request.Request.Email} " +
                    $"due to server error: {confirmResult.Errors.First().Description}."));
            }

            await emailSender.SendAccountRegisteredMessage(new AccountRegisteredMessage()
            {
                To = request.Request.Email,
                Subject = "Successful Account Registration",
                UserName = user.UserName!
            });

            var handler = requestClient.Create(new CreateCartRequest() { UserId = Guid.Parse(user.Id) });
            var response = await handler.GetResponse<ResponseDto>();

            if (!response.Message.IsSucceeded)
            {
                return new(new FailedRpcException(response.Message.ErrorMessage));
            }

            return new(new ConfirmEmailResponse() { Message = "Your email address was successfully confirmed." });
        }
    }
}