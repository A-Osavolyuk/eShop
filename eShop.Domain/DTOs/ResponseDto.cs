namespace eShop.Domain.DTOs;

public class ResponseDto
{
    public string ResultMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
    public object? Result { get; set; }
    public bool IsSucceeded { get; set; } = false;
    public List<string> Errors { get; set; } = [];
}

public class ResponseBuilder
{
    private string resultMessage = "";
    private string errorMessage = "";
    private object? result;
    private bool isSucceeded = false;
    private List<string> errors = [];

    public ResponseBuilder WithResult(object? result)
    {
        result = result;
        return this;
    }

    public ResponseBuilder WithResultMessage(string resultMessage)
    {
        resultMessage = resultMessage;
        return this;
    }

    public ResponseBuilder WithErrorMessage(string errorMessage)
    {
        errorMessage = errorMessage;
        return this;
    }

    public ResponseBuilder Failed()
    {
        isSucceeded = false;
        return this;
    }

    public ResponseBuilder Succeeded()
    {
        isSucceeded = true;
        return this;
    }

    public ResponseBuilder WithErrors(List<string> errors)
    {
        errors.AddRange(errors);
        return this;
    }

    public ResponseBuilder AddError(string error)
    {
        errors.Add(error);
        return this;
    }

    public ResponseDto Build()
    {
        return new ResponseDto()
        {
            IsSucceeded = isSucceeded,
            ErrorMessage = errorMessage,
            Result = result,
            ResultMessage = resultMessage,
            Errors = errors
        };
    }
}


