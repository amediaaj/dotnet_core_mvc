var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Short-circuiting middle-ware
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello");
});

app.Run();
