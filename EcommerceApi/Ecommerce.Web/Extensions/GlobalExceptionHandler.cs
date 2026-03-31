using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Web.Extensions;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
        httpContext.Response.ContentType = "application/json";
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new()
            {
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
                Title = "Internal Server Error",
                Detail = exception.Message
            }
        });
    }
}