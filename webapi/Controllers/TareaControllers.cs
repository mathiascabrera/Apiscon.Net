using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]//Nombre de ruta dinamica

public class TareaController: ControllerBase
{
    ITareasService tareasService;

    public TareaController(ITareasService service)
    {
        tareasService = service;
    }

    [HttpGet]//Indicamos que va a salir por el método Get
    public IActionResult Get()
    {
        return Ok(tareasService.Get());//Retorna un OK de lo que devuelve el "tareasService.Get()"
    }

    /* Ahora vayamos por el método para agregar */
    [HttpPost]//El método va a salir por el método Post
    public IActionResult Post([FromBody] Tarea tarea)//Necesitamos obtener una Tarea, por lo que lo solicitamos desde el cuerpo del Request (frombody) 
    {
        tareasService.Save(tarea);//El Servicio "TareasService" recibe una tarea por parámetro, y no retorna nada por eso escribimos el return a parte.
        return Ok();
    }

    /* Ahora vayamos por el método Actualizar */
    [HttpPut("{id}")]//El método va a salir por el método Put
    //El "id" lo tenemos que recibir por la url, ambos deben tener el mismo nombre y escritos de la misma manera.
    public IActionResult Put(Guid id, [FromBody] Tarea tarea)//Además de la tarea, también necesitamos el id
    {
        tareasService.Update(id, tarea);//El Servicio "TareasService" recibe el id y la Tarea por parámetro
        return Ok();
    }

    //Ahora vamos por el método de Eliminar
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        tareasService.Delete(id);
        return Ok();
    }
    
    //De esta manera finalizamos con los métodos Get() Pop() Pup() y Delete()
}