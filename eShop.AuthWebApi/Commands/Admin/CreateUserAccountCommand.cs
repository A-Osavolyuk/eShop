
using eShop.AuthWebApi.Utilities;
using eShop.Domain.Entities.Admin;

namespace eShop.AuthWebApi.Commands.Admin
{
    public record CreateUserAccountCommand(CreateUserAccountRequest Request) : IRequest<Result<CreateUserAccountResponse>>;

    public class CreateUserAccountCommandHandler(
        AppManager appManager,
        ILogger<CreateUserAccountCommandHandler> logger,
        AuthDbContext context,
        IMapper mapper) : IRequestHandler<CreateUserAccountCommand, Result<CreateUserAccountResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<CreateUserAccountCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async Task<Result<CreateUserAccountResponse>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("create user account");
            try
            {
                logger.LogInformation("Attempting to create user account. Request ID {requestId}", request.Request.RequestId);

                var userId = Guid.NewGuid();
                var user = new AppUser() 
                { 
                    Id = userId.ToString(),
                    Email = request.Request.Email,
                    UserName = request.Request.UserName,
                    PhoneNumber = request.Request.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                };

                var accountResult = await appManager.UserManager.CreateAsync(user);

                if (!accountResult.Succeeded)
                {
                    return logger.LogErrorWithException<CreateUserAccountResponse>(new NotCreatedAccountException(accountResult.Errors), actionMessage, request.Request.RequestId);
                }

                var password = appManager.UserManager.GenerateRandomPassword(18);
                var passwordResult = await appManager.UserManager.AddPasswordAsync(user, password);

                if (!passwordResult.Succeeded) 
                { 
                    return logger.LogErrorWithException<CreateUserAccountResponse>(new NotAddedPasswordException(), actionMessage, request.Request.RequestId);
                }

                await context.PersonalData.AddAsync(new PersonalData()
                {
                    UserId = userId.ToString(),
                    FirstName = request.Request.FirstName,
                    LastName = request.Request.LastName,
                    DateOfBirth = request.Request.DateOfBirth,
                    Gender = request.Request.Gender,
                });

                await context.SaveChangesAsync();

                foreach (var role in request.Request.Roles)
                {
                    var roleExists = await appManager.RoleManager.RoleExistsAsync(role);

                    if (!roleExists)
                    {
                        return logger.LogErrorWithException<CreateUserAccountResponse>(new NotFoundRoleException(role), actionMessage, request.Request.RequestId);
                    }

                    var roleResult = await appManager.UserManager.AddToRoleAsync(user, role);

                    if (!roleResult.Succeeded)
                    {
                        var errorDescription = roleResult.Errors.First().Description;
                        return logger.LogErrorWithException<CreateUserAccountResponse>(new NotAssignRoleException(errorDescription), actionMessage, request.Request.RequestId);
                    }
                }

                // TODO: Permissions logic

                logger.LogInformation("User account was successfully created with temporary password {password}. Request ID {requestID}", password, request.Request.RequestId);

                return new(new CreateUserAccountResponse()
                {
                    Succeeded = true,
                    Message = $"User account was successfully created with temporary password: {password}"
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<CreateUserAccountResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
