// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Catalog.API.Extensions;

namespace Catalog.API.Middlewares;

public class RequestCorrelationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCorrelationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestCorrelationMiddleware> logger)
    {
        var correlationId = context.GetCorrelationId();

        using (logger.BeginScope("{@CorrelationId}", correlationId))
        {
            await _next(context);
        }
    }
}

public static class RequestCorrelationMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCorrelationMiddleware>();
    }
}
