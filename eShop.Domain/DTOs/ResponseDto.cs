namespace eShop.Domain.DTOs;

public class ResponseDTO
{
    public string ResultMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
    public object? Result { get; set; }
    public bool IsSucceeded { get; set; } = false;
    public List<string> Errors { get; set; } = [];
}

public class ResponseBuilder
{
    private string _ResultMessage = "";
    private string _ErrorMessage = "";
    private object? _Result;
    private bool _IsSucceeded = false;
    private List<string> _Errors = [];

    public ResponseBuilder AddResult(object? Result)
    {
        _Result = Result;
        return this;
    }

    public ResponseBuilder AddResultMessage(string ResultMessage)
    {
        _ResultMessage = ResultMessage;
        return this;
    }

    public ResponseBuilder AddErrorMessage(string ErrorMessage)
    {
        _ErrorMessage = ErrorMessage;
        return this;
    }

    public ResponseBuilder Failed()
    {
        _IsSucceeded = false;
        return this;
    }

    public ResponseBuilder Succeeded()
    {
        _IsSucceeded = true;
        return this;
    }

    public ResponseBuilder AddErrors(List<string> Errors)
    {
        _Errors.AddRange(Errors);
        return this;
    }

    public ResponseBuilder AddError(string Error)
    {
        _Errors.Add(Error);
        return this;
    }

    public ResponseDTO Build()
    {
        return new ResponseDTO()
        {
            IsSucceeded = _IsSucceeded,
            ErrorMessage = _ErrorMessage,
            Result = _Result,
            ResultMessage = _ResultMessage,
            Errors = _Errors
        };
    }
}


