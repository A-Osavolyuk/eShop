namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangeUserNameCommand(ChangeUserNameRequest Request) : IRequest<Result<ChangeUserNameResponse>>;

    public class ChangeUserNameCommandHandler(
        ITokenHandler tokenHandler,
        AppManager appManager,
        IValidator<ChangeUserNameRequest> validator,
        ILogger<ChangeUserNameCommandHandler> logger,
        AuthDbContext context) : IRequestHandler<ChangeUserNameCommand, Result<ChangeUserNameResponse>>
    {
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly AppManager appManager = appManager;
        private readonly IValidator<ChangeUserNameRequest> validator = validator;
        private readonly ILogger<ChangeUserNameCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;

        public async Task<Result<ChangeUserNameResponse>> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("change user name with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to change user name with email {email}. Request ID {requestId}", request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<ChangeUserNameResponse>(new FailedValidationException(validationResult.Errors),
                            actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);
                if (user is null)
                {
                    return logger.LogErrorWithException<ChangeUserNameResponse>(new NotFoundUserByEmailException(request.Request.Email),
                        actionMessage, request.Request.RequestId);
                }

                var result = await appManager.UserManager.SetUserNameAsync(user, request.Request.UserName);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ChangeUserNameResponse>(new NotChangedUserNameException(), actionMessage, request.Request.RequestId);
                }

                user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<ChangeUserNameResponse>(new NotFoundUserByEmailException(request.Request.Email),
                        actionMessage, request.Request.RequestId);
                }

                var roles = (await appManager.UserManager.GetRolesAsync(user)).ToList();
                var permissions = (await appManager.PermissionManager.GetUserPermisisonsAsync(user)).ToList();
                var token = tokenHandler.GenerateToken(user!, roles, permissions);

                logger.LogInformation("Successfully change name of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);

                return new(new ChangeUserNameResponse()
                {
                    Message = "Your user name was successfully changed.",
                    Token = token
                });

            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangeUserNameResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
