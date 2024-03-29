using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Middleware;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal sserver error has ocurred"
            };

            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}