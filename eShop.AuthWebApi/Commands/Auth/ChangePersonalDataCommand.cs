namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangePersonalDataCommand(ChangePersonalDataRequest Request) : IRequest<Result<ChangePersonalDataResponse>>;

    public class ChangePersonalDataCommandHandler(
        AppManager appManager,
        IValidator<ChangePersonalDataRequest> validator,
        ILogger<ChangePasswordCommandHandler> logger) : IRequestHandler<ChangePersonalDataCommand, Result<ChangePersonalDataResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly IValidator<ChangePersonalDataRequest> validator = validator;
        private readonly ILogger<ChangePasswordCommandHandler> logger = logger;

        public async Task<Result<ChangePersonalDataResponse>> Handle(ChangePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("change personal data of user with email {0}", request.Request.Email);
            try
            {
                logger.LogInformation("Attempting to change personal data of user with email {email}. Request ID {requestId}",
                    request.Request.Email, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<ChangePersonalDataResponse>(new NotFoundUserByIdException(request.Request.Email),
                    actionMessage, request.Request.RequestId);
                }

                var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null)
                {
                    return logger.LogErrorWithException<ChangePersonalDataResponse>(new NotFoundUserByIdException(request.Request.Email),
                        actionMessage, request.Request.RequestId);
                }

                user.AddPersonalData(request.Request);

                var result = await appManager.UserManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<ChangePersonalDataResponse>(new NotChangedPersonalDataException(), actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Successfully change personal data of user with email {email}. Request ID {requestId}",
                        request.Request.Email, request.Request.RequestId);

                return new(new ChangePersonalDataResponse()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangePersonalDataResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
