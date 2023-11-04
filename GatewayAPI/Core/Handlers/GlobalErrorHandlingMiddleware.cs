using System.Net;
using System.Text.Json;
using GatewayAPI.Extentions.Models.Exceptions;
using KeyNotFoundException = GatewayAPI.Extentions.Models.Exceptions.KeyNotFoundException;
using NotImplementedException = GatewayAPI.Extentions.Models.Exceptions.NotImplementedException;
using UnauthorizedAccessException = GatewayAPI.Extentions.Models.Exceptions.UnauthorizedAccessException;

namespace GatewayAPI.Extentions.Handlers
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case BadRequestException:
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    break;
                case NotImplementedException:
                    message = exception.Message;
                    status = HttpStatusCode.NotImplemented;
                    break;
                case UnauthorizedAccessException:
                case KeyNotFoundException:
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    break;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}