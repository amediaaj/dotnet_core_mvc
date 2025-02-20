using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) => {

    // Streamreader for reading stream in request body
    System.IO.StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    // Convert to dictionary
    Dictionary<string, StringValues> queryDict =
    Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if (queryDict.ContainsKey("age"))
    {
        // string firstName = queryDict["firstName"][0];
        foreach (var age in queryDict["age"])
        {
            await context.Response.WriteAsync($"{age }");
            // await context.Response.WriteAsync($"<h1>{firstName}</h1>");
        }
    }
});

app.Run();
