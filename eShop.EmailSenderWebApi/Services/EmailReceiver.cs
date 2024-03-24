using eShop.Domain.DTOs.Requests;
using MassTransit;

namespace eShop.EmailSenderWebApi.Services
{
    public class EmailReceiver : IConsumer<SendEmailRequest>
    {
        public async Task Consume(ConsumeContext<SendEmailRequest> context)
        {
            
        }
    }
}
