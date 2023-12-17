var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cada vez que se inyecte la interface  IHelloWorldService  se va a crear un nuevo objeto de HelloWorldService internamente, es lo que va a realizar el inyector.
//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();

//Realizamos la inyección de otra manera, utilizamos la "expresión Lambda"
builder.Services.AddScoped<IHelloWorldService>(p=> new HelloWorldService());/* De esta manera podemos inyectar utilizando directamente la clase, sin embargo es ideal utilizar las interfaces, forman parte de las buenas prácticas (SOLID) */
/* De hecho, de esta forma podríamos podríamos agregarle parámetros al constructor, cosa que de la forma en que inyectamos anteriormente no se podría hacer. */

var app = builder.Build();//Después de realizar el Build de la aplicación, se pueden agregar los Middlewares a utilizar

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
