using eShop.Domain.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Interfaces
{
    public interface IStoreService
    {
        public ValueTask RemoveUserAvatarAsync(string userId);
        public ValueTask<string> UploadUserAvatarAsync(string userId, IBrowserFile file);
        public ValueTask<string> GetUserAvatarAsync(string userId);
        public ValueTask<ResponseDto> UploadProductImagesAsync(IReadOnlyList<IBrowserFile> files, Guid productId);
    }
}
