using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Interfaces
{
    public interface IStoreService
    {
        public ValueTask<IEnumerable<string>> AddProductImagesAsync(IReadOnlyList<IBrowserFile> images, Guid productId);
        public ValueTask RemoveUserAvatarAsync(string userId);
        public ValueTask<string> UploadUserAvatarAsync(string userId, IBrowserFile file);
        public ValueTask<string> GetUserAvatarAsync(string userId);
    }
}
