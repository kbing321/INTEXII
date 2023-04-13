namespace INTEXII.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class CspMiddleware
{
    private readonly RequestDelegate _next;

    public CspMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
        await _next(context);
    }
}