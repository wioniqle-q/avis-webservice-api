using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Avis.Common.Middlewares
{
    public class BodyHandlerActions
    {
        private const int MaxJsonBodySize = 1024 * 1024 * 1; // 1MB 

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public BodyHandlerActions(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<BodyHandlerActions>();
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            if (context.Request.ContentLength > MaxJsonBodySize)
            {
                context.Response.StatusCode = 413;
                _logger.LogError($"BodyHandlerActions::Invoke::Request body is too large. Max size is {MaxJsonBodySize} bytes.");
                return;
            }

            await _next(context);
        }
    }
}
