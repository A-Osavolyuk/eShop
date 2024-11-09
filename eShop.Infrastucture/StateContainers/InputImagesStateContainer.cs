using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Infrastructure.StateContainers;

public class InputImagesStateContainer
{
    public event Action? ClearImages;
    public event Func<IReadOnlyList<IBrowserFile>> UploadImages;

    public void OnClearImages()
    {
        ClearImages?.Invoke();
    }

    public IReadOnlyList<IBrowserFile> OnUploadImages()
    {
        var response = UploadImages?.Invoke();
        return response!;
    }
}