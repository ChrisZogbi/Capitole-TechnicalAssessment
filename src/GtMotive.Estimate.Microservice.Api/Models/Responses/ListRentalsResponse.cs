using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Response containing the list of rentals.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListRentalsResponse"/> class.
    /// </remarks>
    /// <param name="rentals">The list of rentals.</param>
    public sealed class ListRentalsResponse(IReadOnlyList<RentalListItemResponse> rentals)
    {
        /// <summary>
        /// Gets the list of rentals.
        /// </summary>
        [Required]
        public IReadOnlyList<RentalListItemResponse> Rentals { get; } = rentals;
    }
}
