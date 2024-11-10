namespace eShop.FilesStorageApi.Commands;

public record DeleteUserAvatarCommand(Guid UserId) : IRequest<Result<DeleteUserAvatarResponse>>;

public class DeleteUserAvatarCommandHandler(
    IStoreService service) : IRequestHandler<DeleteUserAvatarCommand, Result<DeleteUserAvatarResponse>>
{
    private readonly IStoreService service = service;

    public async Task<Result<DeleteUserAvatarResponse>> Handle(DeleteUserAvatarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.DeleteUserAvatarAsync(request.UserId);

            return new(new DeleteUserAvatarResponse()
            {
                Message = "User avatar was deleted successfully"
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}