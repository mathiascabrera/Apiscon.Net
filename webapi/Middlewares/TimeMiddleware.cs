/* La idea es devolver la hora que haya en el Servidor sin importar cuál sea el Request que se esté realizando, sino simplemente se va a analizar el parámetro que venga dentro del Request. */
public class TimeMiddleware
{
    readonly RequestDelegate next;//Esta propiedad nos va ayudar a poder invocar al Middleware que sigue dentro del ciclo y de esta manera construir la lógica de nuestro Middleware de acuerdo a esa secuencia (pipeline). 
    /* El next se encarga del llamado al siguiente Middleware */

    public TimeMiddleware(RequestDelegate nextRequest)
    {
        next = nextRequest;
    }

    /* El método Invoke viene por defecto en todos los Middlewares */
    /* El HttpContext context representa el request */
    public async Task Invoke(HttpContext context)
    {
        //El next invoca al Middleware que sigue, a su vez se obiete la respuesta del último Middleware
        await next(context);

        /* Se procesan todos los request y el que tenga el parámetro "time", le escribimos encima de la respuesta que ya venía, la hora del servidor. */
        //Si el Request, dentro del Query (es decir en los parámetros que se colocan en la URL), existe (Any) algún parámetro que tenga una Key igual a time
        if (context.Request.Query.Any(p => p.Key == "time"))
        {
            //dentro de la respuesta del request escribimos con WriteAsync la hora del servidor
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        }
    }
}


/* Creamos una pequeña clase que nos va ayudar a crear el Middleware dentro de la secuencia establecida en el Program.cs empleando los "use" */
/* Es decir el UseTimeMiddleware dentro de la clase Program.cs */
public static class TimeMiddlewareExtension
{
    /* Recibimos un IApplicationBuilder y lo retornamos */
    public static IApplicationBuilder UseTimeMiddelware(this IApplicationBuilder builder)
    {
        /* Retornamos el mismo builder pero ahora con UseMiddleware<TimeMiddleware>() es decir con nuesto Middleware TimeMiddleware, y luego lo retornamos para que siga con el siguiente Middleware, con la secuencia de comandos. */
        return builder.UseMiddleware<TimeMiddleware>();
    }
}