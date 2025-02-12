﻿using IUpdateHandler = eShop.TelegramBot.Api.Interfaces.IUpdateHandler;

namespace eShop.TelegramBot.Api.Services;

public class UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger) : IUpdateHandler
{
    private readonly ITelegramBotClient bot = bot;
    private readonly ILogger<UpdateHandler> logger = logger;

    public Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    public Task OnErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling error: {exception}", exception);

        return Task.CompletedTask;
    }
}