using eShop.AuthApi.Services.Interfaces;
using eShop.AuthApi.Utilities;
using eShop.Domain.Requests.Auth;
using eShop.Domain.Responses.Auth;

namespace eShop.AuthApi.Commands.Auth
{
    internal sealed record ChangePhoneNumberCommand(ChangePhoneNumberRequest Request) : IRequest<Result<ChangePhoneNumberResponse>>;

    internal sealed class RequestChangePhoneNumberCommandHandler(
        IValidator<ChangePhoneNumberRequest> validator,
        AppManager appManager,
        ILogger<RequestChangePhoneNumberCommandHandler> logger,
        IEmailSender emailSender,
        IConfiguration configuration) : IRequestHandler<ChangePhoneNumberCommand, Result<ChangePhoneNumberResponse>>
    {
        private readonly IValidator<ChangePhoneNumberRequest> validator = validator;
        private readonly AppManager appManager = appManager;
        private readonly ILogger<RequestChangePhoneNumberCommandHandler> logger = logger;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async Task<Result<ChangePhoneNumberResponse>> Handle(ChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("generate change phone number token for user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to generate change phone number token for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogInformationWithException<ChangePhoneNumberResponse>(new FailedValidationException(validationResult.Errors),
                            actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<ChangePhoneNumberResponse>(
                        new NotFoundException($"Cannot find user with email {request.Request.Email}."),
                        actionMessage, request.Request.RequestId);
                }

                var token = await appManager.UserManager.GenerateChangePhoneNumberTokenAsync(user, request.Request.PhoneNumber);

                logger.LogInformation("Successfully generated change phone number token for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                var link = UrlGenerator.ActionLink("/account/change-phone-number", frontendUri, new
                {
                    Token = token,
                    Email = request.Request.Email,
                    PhoneNumber = request.Request.PhoneNumber
                });

                await emailSender.SendChangePhoneNumberMessage(new ChangePhoneNumberMessage()
                {
                    Link = link,
                    To = request.Request.Email,
                    Subject = "Change phone number request",
                    UserName = request.Request.Email,
                    PhoneNumber = request.Request.PhoneNumber
                });

                logger.LogInformation("Successfully sent an email with confirmation to change phone number for user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                return new(new ChangePhoneNumberResponse()
                {
                    Message = "We have sent you an email with instructions."
                });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangePhoneNumberResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
