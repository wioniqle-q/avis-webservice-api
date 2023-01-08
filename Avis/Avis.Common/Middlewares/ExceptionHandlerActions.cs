using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Avis.Common.Middlewares
{
    public class ExceptionHandlerActions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerActions(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<ExceptionHandlerActions>();
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

            var details =  JsonConvert.SerializeObject(new
            {
                Instance = errorFeature switch
                {
                    ExceptionHandlerFeature e => e.Path,
                    _ => "unknown"
                },
                Status = StatusCodes.Status400BadRequest,
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            });

            context.Response.Clear();
            context.Response.ContentType = "application/problem+json";
            context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
            };

            this._logger.LogError($"GLOBAL ERROR HANDLER::HTTP:{context.Response.StatusCode}::{exception.Message}");
            await context.Response.WriteAsync(JsonConvert.SerializeObject(details, Formatting.Indented));
        }
    }
}
