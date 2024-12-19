namespace eShop.Domain.Options;

public class RabbitMqOptions
{
    public string HostUri { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}