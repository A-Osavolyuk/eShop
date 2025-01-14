namespace eShop.TelegramService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/[controller]")]
public class TelegramController(
    IOptions<BotOptions> options,
    ITelegramBotClient bot,
    UpdateHandler updateHandler) : ControllerBase
{
    private readonly ITelegramBotClient bot = bot;
    private readonly UpdateHandler updateHandler = updateHandler;
    private readonly BotOptions options = options.Value;
    
    [HttpGet("setWebhook")]
    public async Task<IActionResult> SetWebHook(CancellationToken ct)
    {
        var webhookUrl = options.WebhookUrl;
        await bot.SetWebhook(webhookUrl, allowedUpdates: [], secretToken: options.Secret, cancellationToken: ct);
        return Ok($"Webhook set to {webhookUrl}");
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update, CancellationToken ct)
    {
        if (Request.Headers["X-Telegram-Bot-Api-Secret-Token"] != options.Secret)
            return Forbid();
        try
        {
            await updateHandler.OnUpdateAsync(bot, update, ct);
        }
        catch (Exception exception)
        {
            await updateHandler.OnErrorAsync(bot, exception, HandleErrorSource.HandleUpdateError, ct);
        }
        return Ok();
    }
}