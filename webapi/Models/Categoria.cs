using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Models;

public class Categoria
{
    //Id del tipo Guid 
    public Guid CategoriaId {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}

    //El peso define qué tan importante es la tarea a la que esta asignada a esta categoría
    public int Peso {get;set;}

    //Lista virtual de tareas que está ignorada y no se incluye dentro del Query que se haga para traer las categorías.
    [JsonIgnore]
    public virtual ICollection<Tarea> Tareas {get;set;}
}