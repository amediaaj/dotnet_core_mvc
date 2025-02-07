using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MiddlewareExample.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // before logic
            System.IO.StreamReader reader = new StreamReader(httpContext.Request.Body);
            string body = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> queryDict =
                Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

            if (queryDict.ContainsKey
                ("firstname") && queryDict.ContainsKey
                ("lastname"))
            {
                string fullname = queryDict
                    ["firstname"] + " " + queryDict
                    ["lastname"] + "\n";
                await httpContext.Response.WriteAsync(fullname);
            }

            await _next(httpContext);
            // after logic
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
