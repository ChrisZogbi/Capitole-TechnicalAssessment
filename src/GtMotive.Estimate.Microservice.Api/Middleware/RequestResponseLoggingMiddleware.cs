using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Api.Middleware
{
    /// <summary>
    /// Logs HTTP request (method, path) on entry and response status code on exit.
    /// </summary>
    public sealed class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResponseLoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger.</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var method = context.Request.Method;
            var path = context.Request.Path.Value ?? string.Empty;

            _logger.LogInformation("Request: {Method} {Path}", method, path);

            await _next(context).ConfigureAwait(false);

            var statusCode = context.Response.StatusCode;
            _logger.LogInformation("Response: {StatusCode} for {Method} {Path}", statusCode, method, path);
        }
    }
}
