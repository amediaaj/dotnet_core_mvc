using MiddlewareExample.CustomMiddleware;
using MiddlewareExample.CustomMiddleWare;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();


app.UseWhen(context => context.Request.Query.ContainsKey("age"), app =>
{
    // Query string parameter here
    app.Use(async (context, next) => {
        await context.Response.WriteAsync("Hello you are old!\n");
        await next();
    });
});

//middleware 1
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Middleware 1 - Starts\n");
    // Forward the context to the next middleware
    await next(context);
    // More code
    await context.Response.WriteAsync("Middleware 1 - Ends\n");
});

//middleware 2
app.UseMiddleware<MyCustomMiddleware>();
// Custom Middleware extension
app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

//middleware 3
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Middleware 3 - Starts\n");
    // More code
});


app.Run(); 
