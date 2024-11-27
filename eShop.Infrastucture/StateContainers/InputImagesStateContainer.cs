namespace eShop.Infrastructure.StateContainers;

public class InputImagesStateContainer
{
    public event Action? OnClearImages;
    public event Func<IReadOnlyList<IBrowserFile>>? OnUploadImages;

    public void ClearImages()
    {
        OnClearImages?.Invoke();
    }

    public IReadOnlyList<IBrowserFile> UploadImages()
    {
        var response = OnUploadImages?.Invoke();
        return response!;
    }
}