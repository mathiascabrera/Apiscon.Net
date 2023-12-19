using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
public class CategoriaController: ControllerBase{
    //Ahora debemos recibir el Servicio de Categorias que es el que posee toda la lógica que vamos a utilizar. Para ello creamos, en la propiedad de la clase, un ICategriaService y lo vamos a recibir desde el Constructor.
    ICategoriaService categoriaService;//Hacemos uso de la Interface, no de la clase, ya que fué la que inyectamos en la clase anterior en el Program.cs

    public CategoriaController(ICategoriaService service)
    {
        categoriaService = service;
    }

    //Ahora pasemos a crear los Endpoints para poder devolver la información
    
    //Por buenas prácticas, y por el standar de OpenAPI, es ideal agregar el método por el cual se va a estar exponiendo  
    [HttpGet]//En este caso le indicamos que va a salir por el método Get
    public IActionResult Get()
    {
        return Ok(categoriaService.Get());//Retorna un OK de lo que devuelve el "categoriaService.Get()"
    }

    /* Ahora vayamos por el método para agregar */
    [HttpPost]//El método va a salir por el método Post
    public IActionResult Post([FromBody] Categoria categoria)//Necesitamos obtener una Categoria, por lo que lo solicitamos desde el cuerpo del Request (frombody) 
    {
        categoriaService.Save(categoria);//El Servicio "CategoriaService" recibe una Categoria por parámetro, y no retorna nada por eso escribimos el return a parte.
        return Ok();
    }

    /* Ahora vayamos por el método Actualizar */
    [HttpPut("{id}")]//El método va a salir por el método Put
    //El "id" lo tenemos que recibir por la url, ambos deben tener el mismo nombre y escritos de la misma manera.
    public IActionResult Put(Guid id, [FromBody] Categoria categoria)//Además de la categoria, también necesitamos el id
    {
        categoriaService.Update(id, categoria);//El Servicio "CategoriaService" recibe el id y la Categoria por parámetro
        return Ok();
    }

    //Ahora vamos por el método de Eliminar
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        categoriaService.Delete(id);
        return Ok();
    }
    
    //De esta manera finalizamos con los métodos Get() Pop() Pup() y Delete()

}