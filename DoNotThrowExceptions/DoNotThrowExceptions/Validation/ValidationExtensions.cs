using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DoNotThrowExceptions.Validation;

public static class ValidationExtensions
{
    public static ProblemDetails ToProblemDetails(this ValidationException exception)
    {
        return new ProblemDetails()
        {
            Status = (int)HttpStatusCode.BadRequest,
            Type = "Server Error",
            Title = "Server Error",
            Detail = exception.Message
        };
    }
}