using System.ComponentModel.DataAnnotations;
using DoNotThrowExceptions.Models;
using LanguageExt.Common;

namespace DoNotThrowExceptions.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public async Task<Result<List<WeatherForecast>>> GetForecastAsync()
    {
        if (Random.Shared.Next(1, 5) < 2)
        {
            // throw new Exception("Ops something happened!");
            var validationException = new ValidationException("Ops something happened!");
            return new Result<List<WeatherForecast>>(validationException);
        }

        return await Task.Run(() =>
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
        });
    }
}