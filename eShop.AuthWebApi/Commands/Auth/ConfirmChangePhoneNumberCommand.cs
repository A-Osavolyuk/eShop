
using eShop.Domain.DTOs.Requests.Auth;
using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Commands.Auth
{
    public record ConfirmChangePhoneNumberCommand(ConfirmChangePhoneNumberRequest Request) : IRequest<Result<ConfirmChangePhoneNumberResponse>>;

    public class ConfirmChangePhoneNumberCommandHandler(
        AppManager appManager,
        ILogger<ConfirmChangePhoneNumberCommandHandler> logger) : IRequestHandler<ConfirmChangePhoneNumberCommand, Result<ConfirmChangePhoneNumberResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<ConfirmChangePhoneNumberCommandHandler> logger = logger;

        public async Task<Result<ConfirmChangePhoneNumberResponse>> Handle(ConfirmChangePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("confirm change phone number of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to confirm change phone number of user with email {email}. Request ID {requestId}.", 
                    request.Request.Email, request.Request.RequestId);
                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is not null)
                {
                    var token = Uri.UnescapeDataString(request.Request.Token);
                    var result = await appManager.UserManager.ChangePhoneNumberAsync(user, request.Request.PhoneNumber, token);

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Successfully changed phone number of user with email {email}. Request ID {requestId}", 
                            request.Request.Email, request.Request.RequestId);
                        return new(new ConfirmChangePhoneNumberResponse() { Message = "Your phone number was successfully changed." });
                    }
                    return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(new NotChangedPhoneNumberException(), actionMessage, request.Request.RequestId);
                }
                return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(new NotFoundUserByEmailException(request.Request.Email), 
                    actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ConfirmChangePhoneNumberResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
