using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Api.Bootstrapping.Middleware;

public class GlobalExceptionHandler(IReadOnlyDictionary<Type, HttpStatusCode> handledExceptionsMap) : IExceptionHandler
{
    private readonly Dictionary<Type, HttpStatusCode> _handledExceptionsMap = CreateStandardHttpExceptionMapping(handledExceptionsMap);

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var hasHttpStatusCodeDefined = _handledExceptionsMap.TryGetValue(exception.GetType(), out HttpStatusCode httpCode);
        if (!hasHttpStatusCodeDefined)
        {
            return false;
        }

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)httpCode;
        var responseBody = JsonSerializer.Serialize(new { error = exception.Message });
        await httpContext.Response.WriteAsync(responseBody, cancellationToken);

        return true;
    }

    private static Dictionary<Type, HttpStatusCode> CreateStandardHttpExceptionMapping(
        IReadOnlyDictionary<Type, HttpStatusCode> handledExceptions)
    {
        Dictionary<Type, HttpStatusCode> exceptionMapping = handledExceptions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        exceptionMapping.TryAdd(typeof(ArgumentException), HttpStatusCode.BadRequest);
        exceptionMapping.TryAdd(typeof(ArgumentNullException), HttpStatusCode.BadRequest);

        return exceptionMapping;
    }
}

