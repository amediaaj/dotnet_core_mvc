var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// endpoint null because this method is called before UseRouting
app.Use(async (context, next) => {
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if(endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}\n");
    }

    await next(context);
});

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
    // add your end points
    endpoints.MapGet("map1", async (context) => {
        await context.Response.WriteAsync("In Map 1");
    });

});

app.UseEndpoints(endpoints => {
    // add your end points
    endpoints.MapPost("map2", async (context) => {
        await context.Response.WriteAsync("In Map 2");
    });

});

app.UseEndpoints(endpoints => {
    // add your end points
    endpoints.Map("map3", async (context) => {
        await context.Response.WriteAsync("In Map 3");
    });

});

app.Run(async context => {
    await context.Response.WriteAsync($"Request recieived at {context.Request.Path}");
});

app.Run();
