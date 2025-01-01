using System.Diagnostics;

namespace Restaurants.API.Middleware;

public class RequestTImeLoggingMiddleware(ILogger<RequestTImeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        await next.Invoke(context);
        stopWatch.Stop();

        if (stopWatch.ElapsedMilliseconds / 1000 > 4)
            logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} {stopWatch.ElapsedMilliseconds}");
    }
}