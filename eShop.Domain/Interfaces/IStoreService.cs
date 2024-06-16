using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Interfaces
{
    public interface IStoreService
    {
        public ValueTask<IEnumerable<string>> AddProductImagesAsync(IReadOnlyList<IBrowserFile> images, Guid productId);
    }
}
