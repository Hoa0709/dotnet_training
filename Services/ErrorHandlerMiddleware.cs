using System.Net;
using System.Text.Json;
using app.Models;

namespace app.Repository
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static async Task HandleErrorAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                Status = "Failure",
                Message = exception.Message
            }.ToString());
        }
    }
}