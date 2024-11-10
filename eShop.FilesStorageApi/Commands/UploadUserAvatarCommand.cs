using eShop.Domain.Exceptions;

namespace eShop.FilesStorageApi.Commands;

public record UploadUserAvatarCommand(IFormFile File, Guid UserId) : IRequest<Result<UploadAvatarResponse>>;

public class UploadUserAvatarCommandHandler(
    IStoreService service) : IRequestHandler<UploadUserAvatarCommand, Result<UploadAvatarResponse>>
{
    private readonly IStoreService service = service;

    public async Task<Result<UploadAvatarResponse>> Handle(UploadUserAvatarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await service.UploadUserAvatarAsync(request.File, request.UserId);

            if (string.IsNullOrEmpty(response))
            {
                return new(new FailedOperationException($"Cannot upload avatar of user with ID {request.UserId}"));
            }

            return new(new UploadAvatarResponse()
            {
                Message = "User avatar was uploaded successfully",
                Uri = response
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}