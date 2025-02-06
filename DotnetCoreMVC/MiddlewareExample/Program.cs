var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//middleware 1
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello");
    // Forward the context to the next middleware
    await next(context);
    // More code
});

//middleware 2
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello again");
    // Forward the context to the next middlewar
    // Or this is terminating middleware

    //await next(context);
    // More code
});


//middleware 3 w/Run i.e. terminating
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello again again");
    // More code
});


app.Run();
