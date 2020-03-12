using System;
using System.Net;
using System.Threading.Tasks;
using Common.DTO;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.WebApiCore.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ApiException ex)
            {
                await this.HandleApiExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = $"Internal Api Error: {exception.Message}" });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }

        private async Task HandleApiExceptionAsync(HttpContext context, ApiException ex)
        {
            var result = JsonConvert.SerializeObject(new ApiExceptionDTO(ex.Message, ex.StatusCode));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsync(result);
        }
    }
}