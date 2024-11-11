namespace eShop.Infrastructure.StateContainers;

public class NotificationsStateContainer
{
    
    public event Func<Task>? OnNotificationsChanged;

    public void ChangeNotificationCount()
    {
        OnNotificationsChanged?.Invoke();
    }
}