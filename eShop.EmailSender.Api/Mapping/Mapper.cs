namespace eShop.EmailSender.Api.Mapping;

public static class Mapper
{
    public static MessageOptions ToMessageOptions(EmailBase email)
    {
        return new MessageOptions()
        {
            Subject = email.Subject,
            To = email.To,
            UserName = email.UserName
        };
    }
}