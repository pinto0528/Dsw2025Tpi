namespace Dsw2025Tpi.Api.Middleware
{
    using Dsw2025Tpi.Application.Exceptions;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var code = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ValidationException => HttpStatusCode.BadRequest,
                InvalidEntityStateException => HttpStatusCode.BadRequest,
                EntityAlreadyExistsException => HttpStatusCode.Conflict,
                ArgumentNullException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };


            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                statusCode = (int)code
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }

}
