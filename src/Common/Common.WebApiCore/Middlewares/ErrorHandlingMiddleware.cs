using System;
using System.Net;
using System.Threading.Tasks;
using Common.DTO;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Common.WebApiCore.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this._logger = logger;
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
            this._logger.Fatal(exception, "Unhandled exception occured");
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = $"Internal Api Error: {exception.Message}" });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }

        private async Task HandleApiExceptionAsync(HttpContext context, ApiException ex)
        {
            this._logger.Fatal(ex, "Unhandled exception occured");
            var result = JsonConvert.SerializeObject(new ApiExceptionDTO(ex.Message, ex.StatusCode));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsync(result);
        }
    }
}