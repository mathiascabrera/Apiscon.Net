//----------------------------------------------------
/* Fragmentos que copiamos de WeatherForecastController, y que necesitamos si o si para crear los controladores */
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]

[Route("api/[controller]")]/* Desde Postman podremos acceder desde la url/api/helloworld */

//----------------------------------------------------

/* Recordemos que los nombres de los controladores deben finalizar con Controller y heredar de ControllerBase */
public class HelloWorldController: ControllerBase
{
    IHelloWorldService helloWorldService;/* Inyeccion */

    public readonly ILogger<HelloWorldController> logger;

    public HelloWorldController(IHelloWorldService helloWorld, ILogger<HelloWorldController> logger2)
    {
        helloWorldService = helloWorld;/* Recibimos por el constructor */
        logger = logger2;
    }

    public IActionResult Get()
    {
        logger.LogInformation("Retornando 'Hello World'.");
        /* Retorna un OK y el GetHelloWorld() */
        return Ok(helloWorldService.GetHelloWorld());
    }
}