using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    /// <summary>
    /// Writes JSON to Response.Body (Stream) to avoid TestServer's PipeWriter not implementing UnflushedBytes (.NET 9).
    /// CanWriteResult and WriteAsync are invoked by the ASP.NET Core MVC pipeline when a controller returns an object, not by our code.
    /// </summary>
    internal sealed class StreamJsonOutputFormatter : IOutputFormatter
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        /// <inheritdoc />
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.Object != null;
        }

        /// <inheritdoc />
        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            await JsonSerializer.SerializeAsync(
                context.HttpContext.Response.Body,
                context.Object,
                context.ObjectType ?? context.Object.GetType(),
                Options).ConfigureAwait(false);
        }
    }
}
