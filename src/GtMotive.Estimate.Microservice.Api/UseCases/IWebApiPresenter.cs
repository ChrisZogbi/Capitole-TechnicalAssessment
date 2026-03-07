using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    /// <summary>
    /// Exposes the HTTP action result for a use case response.
    /// </summary>
    public interface IWebApiPresenter
    {
        /// <summary>
        /// Gets the action result to return from the controller.
        /// </summary>
        IActionResult ActionResult { get; }
    }
}
