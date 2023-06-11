using Cart.Extensions;
using Microsoft.AspNetCore.Authentication;

namespace Cart.Middlewares
{
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
}
