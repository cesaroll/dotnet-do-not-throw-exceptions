using System.ComponentModel.DataAnnotations;
using System.Net;
using DoNotThrowExceptions.Models;
using DoNotThrowExceptions.Services;
using DoNotThrowExceptions.Validation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace DoNotThrowExceptions.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var result = await _weatherForecastService.GetForecastAsync();

        return result.Match<IActionResult>(forecast =>
        {
            return Ok(forecast);
        }, exception =>
        {
            if (exception is ValidationException validationException)
            {
                return BadRequest(validationException.ToProblemDetails());
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        });
    }
}