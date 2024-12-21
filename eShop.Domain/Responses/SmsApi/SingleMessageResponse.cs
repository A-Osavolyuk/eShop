using System.Net;

namespace eShop.Domain.Responses.SmsApi;

public class SingleMessageResponse : ResponseBase
{
    public bool IsSucceeded { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}