using Microsoft.EntityFrameworkCore;//El pproyecto nos compila correctamente debido a que previamente ya habíamos instalado la librería de Microsofot Entity Framework Core
using webapi.Models;

namespace webapi;

//El contexto de Entity Framework siempre va a heredar de "DbContext"
public class TareasContext: DbContext
{
    /* Al DbContext se le debe agregar la configuración de las colecciones que vamos a utilizar.
    Cada una de estas colecciones se convertirán en una tabla en la base de datos a la hora en que nos conectemos a esa base de datos.
    Las colecciones que hemos creado se llaman "Categorias" y "Tareas".  */
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }

    /* "OnModelCreating" es un método de Entity Framework.
    Es en donde tenemos las configuraciones de todas las tablas, a este modelo se lo conoce como "Fluent API", basicamente es la configuración de la base de datos utilizando diferentes métodos de extensión que tiene esta funcionalidad de Entity Framework, llamada "Fluent API". */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //CONFIGURACIÓN DE LAS CATEGORIAS

        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Nombre = "Actividades personales", Peso = 50});
        /* Acá arriba se crean dos datos "Actividades pendientes" y "Actividades personales" */


        modelBuilder.Entity<Categoria>(categoria=> 
        {
            /* De la siguiente forma (con ToTable), establecemos el nombre de la tabla: (En este caso lo definimos como Categoria) */
            categoria.ToTable("Categoria");
            categoria.HasKey(p=> p.CategoriaId);/* Con el "HasKey" establecemos cuál es la clave. */

            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);/* Con "IsRequired()" podemos establecer que el campo sea requerido. */
            /* Con "HasMaxLength()" podemos establecer el máximo de caracteres admitido.  */

            categoria.Property(p=> p.Descripcion).IsRequired(false);/* La Descripcion es "No requerida". */

            categoria.Property(p=> p.Peso);

            categoria.HasData(categoriasInit);/* "HasData()" nos permite agregar datos iniciales, por lo que tendríamos datos con los que vamos a jugar en el momento en el que terminemos de realizar nuestra configuración de la API.  */
        });


        //CONFIGURACIÓN DE LAS TAREAS

        /* En ninguna de las tablas agregamos la Descripción ya que no es Requerida. */

        List<Tarea> tareasInit = new List<Tarea>();

        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pelicula en netflix", FechaCreacion = DateTime.Now });

        modelBuilder.Entity<Tarea>(tarea=>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p=> p.TareaId);

            /* Las siguientes sentencias nos permiten crear la clave foránea, es decir, la relación entre la tabla "Tarea" y "Categoría" */
            tarea.HasOne(p=> p.Categoria).WithMany(p=> p.Tareas).HasForeignKey(p=> p.CategoriaId);
            //Con el "HasOne()" inidicamos a la tabla que esté del lado de uno y con "WithMany()" a la tabla que esté del lado de muchos. Con "HasForeignKey()" establecemos a la clave foránea.

            tarea.Property(p=> p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=> p.Descripcion).IsRequired(false);

            tarea.Property(p=> p.PrioridadTarea);

            tarea.Property(p=> p.FechaCreacion);

            tarea.Ignore(p=> p.Resumen);

            tarea.HasData(tareasInit);

        });

    }

}