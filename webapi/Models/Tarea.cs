using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

public class Tarea
{
    //Id de la tarea
    public Guid TareaId {get;set;}
    //Id de la Categoria
    public Guid CategoriaId {get;set;}
    public string Titulo {get;set;}
    public string Descripcion {get;set;}

    /* Asignamos una prioridad que puede ser Baja, Media o Alta */
    public Prioridad PrioridadTarea {get;set;}

    /* La fecha de creacion, por defecto, va ser la fecha actual. */
    public DateTime FechaCreacion {get;set;}   

     /* Propiedad virtual que basicamente nos permite traer la información de Categoría gracaias al "CategoriaId" */
    public virtual Categoria Categoria {get;set;}

    /* Propiedad resumen que como tal no se usa en la base de datos sino que simplemente se utiliza internamente dentro del modelo para indicar que se va a crear un resumen de la "Descripción" como tal que tiene la tarea cuando la descripción sea muy extensa.    */
    public string Resumen {get;set;}
}

/* La prioridad se encuentra como una enumeración */
public enum Prioridad
{
    Baja,
    Media,
    Alta
}