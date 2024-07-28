namespace eShop.Domain.Common
{
    public record class ActionMessage(string Message, params object?[] Args);
}
