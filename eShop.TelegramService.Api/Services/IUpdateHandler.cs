namespace eShop.TelegramService.Api.Services;

public interface IUpdateHandler
{
    public Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    public Task OnErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken);
}