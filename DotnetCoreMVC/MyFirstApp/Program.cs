var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context) => { 
    string path = context.Request.Path;
    string method = context.Request.Method;

    //if(path == "/path1")...

    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync($"<h1>{path}</h1>");
    await context.Response.WriteAsync($"<h1>{method}</h1>");
});

//app.Run(async (HttpContext context) => {
//    if (1 == 1)
//    {
//        context.Response.StatusCode = 200;
//    }
//    else
//    {
//        context.Response.StatusCode = 400;
//    }
//    context.Response.Headers["MyKey"] = "my value";
//    context.Response.Headers["Content-Type"] = "text/html";
//    await context.Response.WriteAsync("<h1>Hello</h1>");
//    await context.Response.WriteAsync("<h2>World</h2>");

//    //await context.Response.WriteAsync("Hello");
//    //await context.Response.WriteAsync(" World");
//});

app.Run();
