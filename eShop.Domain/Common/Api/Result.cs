namespace eShop.Domain.Common.Api;

public sealed class Result
{
    private Result() { }
    
    public object Value { get; private set; } = null!;
    public bool Success { get; private set; } = false;
    public string Message { get; private set; } = string.Empty;
    public Error? Error { get; private set; } = null;

    public static Result Succeeded(object value, string message)
    {
        return new Result()
        {
            Value = value,
            Success = true,
            Message = message,
        };
    }

    public static Result Failed(string message, string description, string code)
    {
        return new Result()
        {
            Success = false,
            Message = message,
            Error = new Error(message, description, code)
        };
    }
}

public sealed record Error(string Message, string Description, string Code);