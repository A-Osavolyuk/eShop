﻿namespace eShop.Domain.Interfaces.Client;

public interface INotificationService
{
    public ValueTask<int> GetNotificationsCountAsync();
    public ValueTask SetNotificationsCountAsync(int notificationsCount);
    public ValueTask IncrementNotificationsCountAsync();
    public ValueTask DecrementNotificationsCountAsync();
}