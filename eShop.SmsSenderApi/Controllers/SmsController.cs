using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;

namespace eShop.SmsSenderApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class SmsController(IAmazonSimpleNotificationService snsClient) : ControllerBase
{
    private readonly IAmazonSimpleNotificationService snsClient = snsClient;

}
