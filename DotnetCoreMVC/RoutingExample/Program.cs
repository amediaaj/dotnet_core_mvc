var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// enable routing
app.UseRouting();

// endpoint not null if the url and http method matches an endpoint
app.Use(async (context, next) => {
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}\n");
    }

    await next(context);
});

// creating endpoints
app.UseEndpoints(endpoints => {
    // URL must match files.{filename},{extension}
    endpoints.Map("files/{filename}.{extension}", async (context) => {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"In files: {fileName} - {extension}");
    });

    endpoints.Map("employee/profile/{employeename}", async (context) => {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"In employee profile: {employeeName}");
    });

});

// Will only execute if the URL is not one that is mapped
app.Run(async context => {
    await context.Response.WriteAsync($"Request recieived at {context.Request.Path}");
});

app.Run();
