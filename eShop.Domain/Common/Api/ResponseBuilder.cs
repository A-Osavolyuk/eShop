﻿namespace eShop.Domain.Common.Api;

public class ResponseBuilder
{
    private string resultMessage = string.Empty;
    private object? result = null!;
    private bool isSucceeded = false;

    public ResponseBuilder WithResult(object value)
    {
        this.result = value;
        return this;
    }

    public ResponseBuilder WithMessage(string value)
    {
        this.resultMessage = value;
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

    public Response Build()
    {
        var response = Response.Create(resultMessage, result, isSucceeded);
        
        result = null!;
        resultMessage = string.Empty;
        isSucceeded = false;
        
        return response;
    }
}