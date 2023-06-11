using Catalog.API.Extensions;

namespace Catalog.API.Middlewares
{
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
}
