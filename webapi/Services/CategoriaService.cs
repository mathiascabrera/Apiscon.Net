using webapi;
using webapi.Models;

namespace webapi.Services;

public class CategoriaService : ICategoriaService/* Dentro de esta clase va toda la lógica que necesitamos */
{
    TareasContext context;//Necesitamos recibir TareasContext para implementar el método Get()

    //Ahora recibimos a TareasContext por el Constructor
    /* Las dependencias que tengamos en .NET se pueden inyectar en cualquier parte no necesariamente tiene que ser dentro del Controlador, en este caso lo vamos a realizar en esta Clase. */
    public CategoriaService(TareasContext dbcontext){
        context = dbcontext;
    }
    public IEnumerable<Categoria> Get()
    {
        return context.Categorias;
    }


    /* Método para guardar: */

    //Sin Asíncrono:
    /* public void Save(Categoria categoria)
    {
        context.Add(categoria);
        context.SaveChanges();
    } */

    //Con Asíncrono:
    public async Task Save(Categoria categoria)
    {
        context.Add(categoria);
        await context.SaveChangesAsync();
    }

    /* Método para realizar el Edit: */
    public async Task Update(Guid id, Categoria categoria)
    {
        //Busca la Categoria por el id
        var categoriaActual = context.Categorias.Find(id);

        if(categoriaActual != null)//Si es distinto a null es porque lo encontró
        {
            /* Si se encontró la categoria, entonces reemplazamos el Nombre, la Descripcion y el Peso. */
            categoriaActual.Nombre = categoria.Nombre;
            categoriaActual.Descripcion = categoria.Descripcion;
            categoriaActual.Peso = categoria.Peso;

            //Para garantizar que todo se haya guardado correctamente
            await context.SaveChangesAsync();
        }
    }

    /* Método para Eliminar: */
    public async Task Delete(Guid id)
    {
        //Busca la Categoria por el id
        var categoriaActual = context.Categorias.Find(id);

        if(categoriaActual != null)//Si es distinto a null es porque lo encontró
        {
            context.Remove(categoriaActual);//Eliminamos a la categoria actual

            //Para garantizar que todo se haya guardado correctamente
            await context.SaveChangesAsync();
        }
    }
    

}

/* No nos olvidemos de crear siempre las interfaces */
public interface ICategoriaService
{
    IEnumerable<Categoria> Get();
    Task Save(Categoria categoria);
    Task Update(Guid id, Categoria categoria);
    Task Delete(Guid id);
}