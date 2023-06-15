// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Microsoft.AspNetCore.Authentication;

namespace Cart.Middlewares;

public class AccessTokenMiddleware
{
    private readonly RequestDelegate _next;

    public AccessTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<AccessTokenMiddleware> logger)
    {
        var accessToken = await context.GetTokenAsync("access_token");

        logger.LogInformation($"Access token: {accessToken}");

        await _next(context);
    }
}

public static class AccessTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseAccessTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AccessTokenMiddleware>();
    }
}
