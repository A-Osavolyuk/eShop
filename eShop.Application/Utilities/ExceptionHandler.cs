﻿using System.Net;
using eShop.Domain.Common.Api;
using eShop.Domain.Exceptions.Interfaces;

namespace eShop.Application.Utilities;

public static class ExceptionHandler
{
    public static ActionResult HandleException(Exception exception)
    {
        return exception switch
        {
            INotFoundException => new NotFoundObjectResult(new ResponseBuilder()
                .Failed()
                .WithMessage(exception.Message)
                .Build()),

            IBadRequestException => new BadRequestObjectResult(new ResponseBuilder()
                .Failed()
                .WithMessage(exception.Message)
                .Build()),

            IFailedValidationException => new BadRequestObjectResult(new ResponseBuilder()
                .Failed()
                .WithMessage(exception.Message)
                .Build()),

            _ => new ObjectResult(new ResponseBuilder()
                .Failed()
                .WithMessage(exception.Message)
                .Build()) { StatusCode = 500 }
        };
    }
}