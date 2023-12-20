using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Acá se establece una dependencia, los servicios son las dependencias. Agrega diferentes servicios que serán utilizados por los componentes internos de la aplicación 
builder.Services.AddSwaggerGen();

//Configuramos Entity Framework. Entre <> va el Context. El ("") es el conexion string para conectarnos a la base de datos.
builder.Services.AddSqlServer<TareasContext>("Data Source=DESKTOP-GRI1MOM;Initial Catalog=TareasDb;user id=matydev;password=matydev;TrustServerCertificate=True");


//Cada vez que se inyecte la interface  IHelloWorldService  se va a crear un nuevo objeto de HelloWorldService internamente, es lo que va a realizar el inyector.
//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();

//Realizamos la inyección de otra manera, utilizamos la "expresión Lambda"
builder.Services.AddScoped<IHelloWorldService>(p=> new HelloWorldService());/* De esta manera podemos inyectar utilizando directamente la clase, sin embargo es ideal utilizar las interfaces, forman parte de las buenas prácticas (SOLID) */
/* De hecho, de esta forma podríamos podríamos agregarle parámetros al constructor, cosa que de la forma en que inyectamos anteriormente no se podría hacer. */


/* Aquí realizamos las inyecciones de dependencias  */
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareasService, TareasService>();
/* Con esto ya tenemos la configuración de inyección de dependencias */


var app = builder.Build();//Después de realizar el Build de la aplicación, se pueden agregar los Middlewares a utilizar

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//La configuración del Swagger está para el ambiente de Development, es decir, para el "appsettings.Development.json", esto viene por defecto de esta manera ya que este documento no debería estar en Producción, porque si un Hacker logra ingresar a nuesta API y extraer la documentación que se encuentra en Producción va a poder conocer cómo funciona la API internamente y todos los métodos y funciones que tiene   
{
    app.UseSwagger();//Esto es un Middleware
    app.UseSwaggerUI();//Esto es un Middleware
}

app.UseHttpsRedirection();//Esto es un Middleware

//app.UseCors();

app.UseAuthorization();//Esto es un Middleware

//app.UseWelcomePage();//este Middleare agrega una página de bienvenida cada vez que un cliente ingresa a la API.

//app.UseTimeMiddelware();/* Realizamos el UseTimeMiddelware() que acabamos de crear eb Middleare/TimeMiddleware.cs */

app.MapControllers();

app.Run();
