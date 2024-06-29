using CarRental.Shared.Middleware;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CarRental.Shared.Extensions;


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

