namespace eShop.Infrastructure.State;

public class NotificationsStateContainer
{
    
    public event Func<Task>? OnNotificationsChanged;

    public void ChangeNotificationCount()
    {
        OnNotificationsChanged?.Invoke();
    }
}