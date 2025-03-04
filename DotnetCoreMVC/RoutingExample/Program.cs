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

    // Constraint
    // "{parameter:constraint}"
    // Default Parameter
    // "{parameter=dafault_value}"
    // Eg: employee/profile/defaultname
    endpoints.Map("employees/profile/{employeename:alpha:length(4, 7)=defaultname}", async (context) => {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"In employees profile: {employeeName}");
    });

    // Constraint
    // "{parameter:constraint}"
    // Constraint
    // "{parameter:type}"
    // Optional Route Parameter
    // "{parameter?}"
    // Eg: products/details/1
    endpoints.Map("products/details/{id:int:range(1, 1000)?}", async (context) => {
        // Will contain key id only if value for id was supplied
        if(context.Request.RouteValues.ContainsKey("id"))
        {
            int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"In products details: {id}");
        } else
        {
            await context.Response.WriteAsync($"In products details: id is not supplied");
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

    //sales-report/2030/apr
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context => {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if(month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"sales report - {year} - {month}");
        }
        else
        {
            await context.Response.WriteAsync($"{month} is not allowed.");
        }
        
    });

});

// Will only execute if the URL is not one that is mapped
app.Run(async context => {
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});

app.Run();
