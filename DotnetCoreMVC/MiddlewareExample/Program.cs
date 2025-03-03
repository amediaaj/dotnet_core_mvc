using MiddlewareExample.CustomMiddleware;
using MiddlewareExample.CustomMiddleWare;

var builder = WebApplication.CreateBuilder(args);
// Register custom middleware to Services
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();


/******************* Begin Request Pipeline **************************/
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

// middleware 2
// Use without extension
// app.UseMiddleware<MyCustomMiddleware>();

// Custom middleware extension
app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

//middleware 3 - Short circuiting or terminating middleware
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Middleware 3 - Starts\n");
});

/******************** End Request Pipeline *********************/


app.Run(); 
