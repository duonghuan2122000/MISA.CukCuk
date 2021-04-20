using Microsoft.AspNetCore.Http;
using MISA.Core.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message;
            if (exception is CustomerException)
            {
                status = HttpStatusCode.BadRequest;
            }
            var response = new
            {
                devMsg = exception.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
                MISACode = "002",
                Data = exception.Data
            };

            /*var stackTrace = String.Empty;
            message = exception.Message;
            var exceptionType = exception.GetType();*/
            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}
