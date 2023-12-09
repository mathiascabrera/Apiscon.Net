using System.Security.Cryptography.X509Certificates;
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

    /* Creamos una coleccion estatica */
    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

    /* Constructor:  */
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        /* El método Any devuelve un booleano, determina si tiene elementos u registros. En este caso en la condicion del if es si no tiene ningún registro. */
        if (ListWeatherForecast == null || !ListWeatherForecast.Any())
        {
            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
        .ToList();
        }
    }

    /* Se crea un Endpoint al que le llamaremos GetWeatherForecast, el cual retorna un rango de informacion relacionada a la fecha, a la temperatura y un resumen de datos aleatorios. */
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return ListWeatherForecast;

    }

    /* Metodo POST: */
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        ListWeatherForecast.Add(weatherForecast);
        return Ok();
    }

    /* Metodo DELET: */
    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        ListWeatherForecast.RemoveAt(index);
        return Ok();

    }
}
