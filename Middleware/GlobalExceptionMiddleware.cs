using System.Net;
using Microsoft.AspNetCore.Mvc;
using rutaApiv1.Models;

namespace rutaApiv1.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var traceId = context.TraceIdentifier;
            
            var response = new ErrorResponse(
                traceId: traceId,
                message: "An error occurred while processing your request",
                detail: exception.Message
            );

            _logger.LogError(exception, "Request {TraceId} failed with error: {Message}", traceId, exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

    // Extension method para facilitar el registro del middleware
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}