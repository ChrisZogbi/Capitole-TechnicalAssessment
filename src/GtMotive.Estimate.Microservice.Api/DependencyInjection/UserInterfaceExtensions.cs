using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    /// <summary>
    /// Registers services for the user interface (API) layer.
    /// </summary>
    public static class UserInterfaceExtensions
    {
        /// <summary>
        /// Registers presenters and other UI-related services.
        /// Use cases now return Result types; handlers map results to IActionResult.
        /// This method is kept for future presenter registration or other UI setup.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            return services;
        }
    }
}
