namespace eShop.AuthApi.Commands.Admin;

internal sealed record CreateRoleCommand(CreateRoleRequest Request) : IRequest<Result<CreateRoleResponse>>;

internal sealed class CreateRoleCommandHandler(
    AppManager appManager,
    AuthDbContext context) : IRequestHandler<CreateRoleCommand, Result<CreateRoleResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly AuthDbContext context = context;

    public async Task<Result<CreateRoleResponse>> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var isRoleExists = await appManager.RoleManager.FindByNameAsync(request.Request.Name);

        if (isRoleExists is not null)
        {
            return new(new CreateRoleResponse() { Message = "Role already exists.", Succeeded = true });
        }

        var result = await appManager.RoleManager.CreateAsync(new IdentityRole() { Name = request.Request.Name });

        if (!result.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot create role due to server error: {result.Errors.First().Description}"));
        }

        return new(new CreateRoleResponse() { Message = "Role was successfully created", Succeeded = true });
    }
}