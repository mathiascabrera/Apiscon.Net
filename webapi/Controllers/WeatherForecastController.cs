using Microsoft.AspNetCore.Mvc;

/* Esto es un demo del método GET */

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]/* Esto define la ruta de cómo podemos consumir este Endpoint. La ruta recibe el mismo nombre que el controlador: WeatherForecastController*/
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /* Se crea un Endpoint al que le llamaremos GetWeatherForecast, el cual retorna un rango de informacion relacionada a la fecha, a la temperatura y un resumen de datos aleatorios. */
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
