using Newtonsoft.Json;

namespace eShop.Domain.Common.Api;

public class Response
{
    private Response() {}
    public string Message { get; private init; } = string.Empty;
    public object? Result { get; private init; } = null!;
    public bool Success { get; private init; } = false;

    public static Response Create(string message, object? result = null, bool isSucceeded = false)
    {
        return new Response()
        {
             Message = message,
             Result = result,
             Success = isSucceeded
        };
    }

    public TValue DeserializeResult<TValue>()
    {
        var json = JsonConvert.SerializeObject(Result!);
        var result = JsonConvert.DeserializeObject<TValue>(json);
        
        return result!;
    }
}