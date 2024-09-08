namespace eShop.AuthWebApi.Commands.Admin
{
    public record CreateRoleCommand(CreateRoleRequest Request) : IRequest<Result<CreateRoleResponse>>;

    public class CreateRoleCommandHandler(
        AppManager appManager,
        AuthDbContext context,
        IValidator<CreateRoleRequest> validator,
        ILogger<CreateRoleRequest> logger) : IRequestHandler<CreateRoleCommand, Result<CreateRoleResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly AuthDbContext context = context;
        private readonly IValidator<CreateRoleRequest> validator = validator;
        private readonly ILogger<CreateRoleRequest> logger = logger;

        public async Task<Result<CreateRoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("create role with name {0}", request.Request.Name);
            try
            {
                logger.LogInformation("Attempting to create role with name {roleName}. Request ID {requestId}",
                    request.Request.Name, request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<CreateRoleResponse>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var isRoleExists = await appManager.RoleManager.FindByNameAsync(request.Request.Name);

                if (isRoleExists is not null)
                {
                    return new(new CreateRoleResponse() { Message = "Role already exists.", Succeeded = true });
                }

                var result = await appManager.RoleManager.CreateAsync(new IdentityRole() { Name = request.Request.Name });

                if (!result.Succeeded)
                {
                    return logger.LogErrorWithException<CreateRoleResponse>(new NotCreatedRoleException(validationResult.Errors.First().ErrorMessage),
                        actionMessage, request.Request.RequestId);
                }

                logger.LogInformation("Role with name {roleName} was successfully created. Request ID {requestId}", request.Request.Name, request.Request.RequestId);
                return new(new CreateRoleResponse() { Message = "Role was successfully created", Succeeded = true });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<CreateRoleResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
