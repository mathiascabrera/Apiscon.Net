/* Solamente vamos a crear el método GET(), y como reto tenemos que crear los otros métodos */
using webapi;
using webapi.Models;

namespace webapi.Services;

public class TareasService:ITareasService
{
    TareasContext context;
    public TareasService(TareasContext dbcontext)
    {
        context = dbcontext;
    }

    public IEnumerable<Tarea> Get()
    {
        return context.Tareas;
    }

    //Método para guardar:
    public async Task Save(Tarea tarea)
    {
        context.Add(tarea);
        await context.SaveChangesAsync();
    }

    /* Método para realizar el Edit: */
    public async Task Update(Guid id, Tarea tarea)
    {
        //Busca la Tarea por el id
        var tareaActual = context.Tareas.Find(id);

        if(tareaActual != null)//Si es distinto a null es porque lo encontró
        {
            /* Si se encontró la categoria, entonces reemplazamos el Nombre, la Descripcion y el Peso. */
            tareaActual.Titulo = tarea.Titulo;
            tareaActual.Descripcion = tarea.Descripcion;
            tareaActual.PrioridadTarea = tarea.PrioridadTarea;
            tareaActual.FechaCreacion = tarea.FechaCreacion;
            tareaActual.Resumen = tarea.Resumen;
            tareaActual.CategoriaId = tarea.CategoriaId;

            //Para garantizar que todo se haya guardado correctamente
            await context.SaveChangesAsync();
        }
    }

     /* Método para Eliminar: */
    public async Task Delete(Guid id)
    {
        //Busca la Tarea por el id
        var tareaActual = context.Tareas.Find(id);

        if(tareaActual != null)//Si es distinto a null es porque lo encontró
        {
            context.Remove(tareaActual);//Eliminamos a la tarea actual

            //Para garantizar que todo se haya guardado correctamente
            await context.SaveChangesAsync();
        }
    }

}

public interface ITareasService
{
    IEnumerable<Tarea> Get();
    Task Save(Tarea tarea);
    Task Update(Guid id, Tarea tarea);
    Task Delete(Guid id);
}