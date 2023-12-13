var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseWelcomePage();//este Middleare agrega una página de bienvenida cada vez que un cliente ingresa a la API.

app.MapControllers();

app.Run();
