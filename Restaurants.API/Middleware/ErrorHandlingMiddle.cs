using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notfoundException)
        {
            logger.LogWarning(notfoundException.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notfoundException.Message);
        }
        catch (ForbiddenException forbiddenException)
        {
            logger.LogWarning(forbiddenException.Message);
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbiddenException.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}