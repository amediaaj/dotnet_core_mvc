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

    // Default Parameter
    // "{parameter=dafault_value}"
    // Eg: employee/profile/defaultname
    endpoints.Map("employees/profile/{employeename=defaultname}", async (context) => {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"In employees profile: {employeeName}");
    });

    // Constraint
    // "{parameter:type}"
    // Optional Route Parameter
    // "{parameter?}"
    // Eg: products/details/1
    endpoints.Map("products/detail/{id:int?}", async (context) => {
        // Will contain key id only if value for id was supplied
        if(context.Request.RouteValues.ContainsKey("id"))
        {
            int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"In products detail: {id}");
        } else
        {
            await context.Response.WriteAsync($"In products detail: id is not supplied");
        }
    });

    // Constraint
    // "{parameter:type}"
    //Eg: daily-digest-report/{reportdate}
    endpoints.Map("daily-digest-report/{reportdate:datetime}", async context => {
        DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"In daily-digest-report: {reportDate.ToShortDateString()}");
    });

    // Eg: cities/cityid
    endpoints.Map("cities/{cityid:guid}", async context => {
        // Tools => Create GUID => 4. Registry Format

        // ! indicates the value cannot be null
        Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
        await context.Response.WriteAsync($"City information: {cityId}");
    });

});

// Will only execute if the URL is not one that is mapped
app.Run(async context => {
    await context.Response.WriteAsync($"Request recieived at {context.Request.Path}");
});

app.Run();
