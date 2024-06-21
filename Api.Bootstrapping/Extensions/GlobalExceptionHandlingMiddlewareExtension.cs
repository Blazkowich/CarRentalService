using Api.Bootstrapping.Middleware;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Api.Bootstrapping.Extensions;


public static class GlobalExceptionHandlingMiddlewareExtension
{
    public static IServiceCollection ConfigureExceptionHandlingMiddleware(
        this IServiceCollection services,
        IReadOnlyDictionary<Type, HttpStatusCode> handledExceptionsMap = null)
    {
        var globalExceptionHandler = new GlobalExceptionHandler(handledExceptionsMap ?? new Dictionary<Type, HttpStatusCode>());
        services.AddSingleton(globalExceptionHandler);

        return services;
    }
}

