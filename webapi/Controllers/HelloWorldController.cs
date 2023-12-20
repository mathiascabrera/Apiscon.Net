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

    //Recibimos el contexto, el que nos va a permitir conectarnos a la Base de Datos 
    TareasContext dbcontext;

    public HelloWorldController(IHelloWorldService helloWorld, ILogger<HelloWorldController> logger2, TareasContext db)
    {
        helloWorldService = helloWorld;/* Recibimos por el constructor */
        logger = logger2;
        dbcontext = db;
    }
    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("Retornando 'Hello World'.");
        /* Retorna un OK y el GetHelloWorld() */
        return Ok(helloWorldService.GetHelloWorld());
    }

    //Creamos un Endpoint para comprobar que la base de datos fué creada
    [HttpGet]//Lo vamos a lanzar mediante Get
    [Route("createdb")]//Le asignamos una ruta a nuestro Endpoint
    public IActionResult CreateDataBase()
    {
        dbcontext.Database.EnsureCreated();//De esta forma vamos a verificar que la base de datos se va a crear correctamente.
        return Ok();
    }

    //De esta forma tenemos un método para conectarnos a la base de datos y asegurarnos de que esté creada y si no está creada, la va a crear con toda la configuración que tenemos en "TareasContext.cs"
}