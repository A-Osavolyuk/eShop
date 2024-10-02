namespace eShop.AuthWebApi.Commands.Auth
{
    public record ChangePersonalDataCommand(ChangePersonalDataRequest Request) : IRequest<Result<ChangePersonalDataResponse>>;

    public class ChangePersonalDataCommandHandler(
        AppManager appManager,
        IValidator<ChangePersonalDataRequest> validator,
        ILogger<ChangePasswordCommandHandler> logger,
        AuthDbContext context,
        IMapper mapper) : IRequestHandler<ChangePersonalDataCommand, Result<ChangePersonalDataResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly IValidator<ChangePersonalDataRequest> validator = validator;
        private readonly ILogger<ChangePasswordCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;

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

                var personalData = new PersonalData()
                {
                    UserId = user.Id,
                    FirstName = request.Request.FirstName,
                    LastName = request.Request.LastName,
                    Gender = request.Request.Gender,
                    DateOfBirth = request.Request.DateOfBirth!.Value
                };

                await context.PersonalData.AddAsync(personalData);
                await context.SaveChangesAsync();

                logger.LogInformation("Successfully change personal data of user with email {email}. Request ID {requestId}",
                        request.Request.Email, request.Request.RequestId);

                return new(mapper.Map<ChangePersonalDataResponse>(personalData));
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ChangePersonalDataResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
