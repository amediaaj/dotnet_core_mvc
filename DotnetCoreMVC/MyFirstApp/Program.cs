var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) => { 
    string path = context.Request.Path;
    string method = context.Request.Method;

    if (method == "GET")
    {
        context.Response.Headers["Content-Type"] = "text/html";
        if (context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }
    }
});

app.Run();
