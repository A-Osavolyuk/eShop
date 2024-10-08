﻿namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetTwoFactorAuthenticationStateQuery(string Email) : IRequest<Result<TwoFactorAuthenticationStateResponse>>;
    public class GetTwoFactorAuthenticationStateQueryHandler(
        AppManager appManager,
        ILogger<GetTwoFactorAuthenticationStateQueryHandler> logger) : IRequestHandler<GetTwoFactorAuthenticationStateQuery, Result<TwoFactorAuthenticationStateResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<GetTwoFactorAuthenticationStateQueryHandler> logger = logger;

        public async Task<Result<TwoFactorAuthenticationStateResponse>> Handle(GetTwoFactorAuthenticationStateQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("get 2FA state of user with email {0}", request.Email);
            try
            {
                logger.LogInformation("Attempting get 2FA state of user with email {email}", request.Email);
                var user = await appManager.UserManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<TwoFactorAuthenticationStateResponse>(new NotFoundUserByEmailException(request.Email), actionMessage);
                }

                logger.LogInformation("Successfully got 2FA state of user with email {email}", request.Email);
                return new(new TwoFactorAuthenticationStateResponse() { TwoFactorAuthenticationState = user.TwoFactorEnabled });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<TwoFactorAuthenticationStateResponse>(ex, actionMessage);
            }
        }
    }
}
