using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to list rentals with optional filter by active state.
    /// </summary>
    public sealed class ListRentalsRequest : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the optional filter: true = only active rentals, false = only returned, null/omit = all.
        /// </summary>
        public bool? ActiveOnly { get; set; }
    }
}
