using eShop.Domain.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Interfaces
{
    public interface IStoreService
    {
        public ValueTask<ResponseDto> RemoveUserAvatarAsync(string userId);
        public ValueTask<ResponseDto> UploadUserAvatarAsync(string userId, IBrowserFile file);
        public ValueTask<ResponseDto> GetUserAvatarAsync(string userId);
        public ValueTask<ResponseDto> UploadProductImagesAsync(IReadOnlyList<IBrowserFile> files, Guid productId);
    }
}
