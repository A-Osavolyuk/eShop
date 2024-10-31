namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetPhoneNumberQuery(string Email) : IRequest<Result<GetPhoneNumberResponse>>;

    public class GetPhoneNumberQueryHandler(
        AppManager appManager,
        ILogger<GetPhoneNumberQueryHandler> logger) : IRequestHandler<GetPhoneNumberQuery, Result<GetPhoneNumberResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetPhoneNumberQueryHandler> logger = logger;

        public async Task<Result<GetPhoneNumberResponse>> Handle(GetPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find phone number of user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting to find a phone number of user with email {email}", request.Email);
                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogInformationWithException<GetPhoneNumberResponse>(
                        new NotFoundException($"Cannot find user with email {request.Email}."), actionMessage);
                }

                logger.LogInformation("Successfully found a phone number of user with email {email}", request.Email);
                return new(new GetPhoneNumberResponse()
                {
                    PhoneNumber = user.PhoneNumber!
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<GetPhoneNumberResponse>(ex, actionMessage);
            }
        }
    }
}
