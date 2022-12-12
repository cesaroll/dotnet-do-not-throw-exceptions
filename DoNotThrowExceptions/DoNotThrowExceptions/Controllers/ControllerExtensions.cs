using System.ComponentModel.DataAnnotations;
using System.Net;
using DoNotThrowExceptions.Validation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace DoNotThrowExceptions.Controllers;

public static class ControllerExtensions
{
    public static IActionResult ToOk<TResult, TContract>(
        this Result<TResult> result,
        Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(obj =>
        {
            var response = mapper(obj);
            return new OkObjectResult(response);
        }, exception =>
        {
            if (exception is ValidationException validationException)
            {
                return new BadRequestObjectResult(validationException.ToProblemDetails());
            }

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        });
    }
}