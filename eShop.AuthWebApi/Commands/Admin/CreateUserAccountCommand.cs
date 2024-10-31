using eShop.AuthWebApi.Utilities;
using eShop.Domain.Entities;
using eShop.Domain.Entities.Admin;
using eShop.Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace eShop.AuthWebApi.Commands.Admin
{
    public record CreateUserAccountCommand(CreateUserAccountRequest Request)
        : IRequest<Result<CreateUserAccountResponse>>;

    public class CreateUserAccountCommandHandler(
        AppManager appManager,
        ILogger<CreateUserAccountCommandHandler> logger,
        AuthDbContext context,
        IMapper mapper,
        IConfiguration configuration) : IRequestHandler<CreateUserAccountCommand, Result<CreateUserAccountResponse>>
    {
        private readonly AppManager appManager = appManager;
        private readonly ILogger<CreateUserAccountCommandHandler> logger = logger;
        private readonly AuthDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;
        private readonly string defaultRole = configuration["DefaultValues:DeafultRole"]!;

        private readonly List<string> defaultPermissions =
            configuration.GetValue<List<string>>("DefaultValues:DeafultPermissions")!;

        public async Task<Result<CreateUserAccountResponse>> Handle(CreateUserAccountCommand request,
            CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("create user account");
            try
            {
                logger.LogInformation("Attempting to create user account. Request ID {requestId}",
                    request.Request.RequestId);

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
                    return logger.LogErrorWithException<CreateUserAccountResponse>(
                        new FailedOperationException(
                            $"Cannot create account due to server error: {accountResult.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
                }

                var password = appManager.UserManager.GenerateRandomPassword(18);
                var passwordResult = await appManager.UserManager.AddPasswordAsync(user, password);

                if (!passwordResult.Succeeded)
                {
                    return logger.LogErrorWithException<CreateUserAccountResponse>(
                        new FailedOperationException(
                            $"Cannot add password to user account due ti server error: {passwordResult.Errors.First().Description}."),
                        actionMessage, request.Request.RequestId);
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

                if (request.Request.Roles.Any())
                {
                    foreach (var role in request.Request.Roles)
                    {
                        var roleExists = await appManager.RoleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            return logger.LogInformationWithException<CreateUserAccountResponse>(
                                new NotFoundException($"Cannot find role {role}."),
                                actionMessage, request.Request.RequestId);
                        }

                        var roleResult = await appManager.UserManager.AddToRoleAsync(user, role);

                        if (!roleResult.Succeeded)
                        {
                            return logger.LogErrorWithException<CreateUserAccountResponse>(
                                new FailedOperationException($"Cannot add user with ID {user.Id} to role {role} " +
                                                             $"due to server error: {roleResult.Errors.First().Description}."),
                                actionMessage, request.Request.RequestId);
                        }
                    }
                }
                else
                {
                    var roleResult = await appManager.UserManager.AddToRoleAsync(user, defaultRole);

                    if (!roleResult.Succeeded)
                    {
                        return logger.LogErrorWithException<CreateUserAccountResponse>(
                            new FailedOperationException($"Cannot add user with ID {user.Id} to role {defaultRole} " +
                                                         $"due to server error: {roleResult.Errors.First().Description}."),
                            actionMessage, request.Request.RequestId);
                    }
                }

                if (request.Request.Permissions.Any())
                {
                    foreach (var permission in request.Request.Permissions)
                    {
                        var permissionExists =
                            await context.Permissions.AsNoTracking().AnyAsync(x => x.Name == permission);

                        if (!permissionExists)
                        {
                            return logger.LogInformationWithException<CreateUserAccountResponse>(
                                new NotFoundException($"Cannot find permission {permission}."),
                                actionMessage, request.Request.RequestId);
                        }

                        var permissionResult =
                            await appManager.PermissionManager.IssuePermissionToUserAsync(user, permission);

                        if (!permissionResult.Succeeded)
                        {
                            return logger.LogErrorWithException<CreateUserAccountResponse>(
                                new FailedOperationException(
                                    $"Cannot issue permission {permission} to user with ID {user.Id} due to " +
                                    $"server error: {permissionResult.Errors.First().Description}."),
                                actionMessage, request.Request.RequestId);
                        }
                    }
                }
                else
                {
                    foreach (var permission in defaultPermissions)
                    {
                        var permissionResult =
                            await appManager.PermissionManager.IssuePermissionToUserAsync(user, permission);

                        if (!permissionResult.Succeeded)
                        {
                            return logger.LogErrorWithException<CreateUserAccountResponse>(
                                new FailedOperationException(
                                    $"Cannot issue permission {permission} to user with ID {user.Id} due to " +
                                    $"server error: {permissionResult.Errors.First().Description}."),
                                actionMessage, request.Request.RequestId);
                        }
                    }
                }

                logger.LogInformation(
                    "User account was successfully created with temporary password {password}. Request ID {requestID}",
                    password, request.Request.RequestId);

                return new(new CreateUserAccountResponse()
                {
                    Succeeded = true,
                    Message = $"User account was successfully created with temporary password: {password}"
                });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<CreateUserAccountResponse>(ex, actionMessage,
                    request.Request.RequestId);
            }
        }
    }
}