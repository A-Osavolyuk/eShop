namespace eShop.Infrastructure.StateContainers;

public class InputImagesStateContainer
{
    public event Action? ClearImages;

    public void OnClearImages()
    {
        ClearImages?.Invoke();
    }
}