using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals
{
    /// <summary>
    /// Output for the list of rentals.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListRentalsOutput"/> class.
    /// </remarks>
    /// <param name="rentals">The list of rentals.</param>
    public sealed class ListRentalsOutput(IReadOnlyList<RentalSummary> rentals)
    {
        /// <summary>
        /// Gets the list of rentals.
        /// </summary>
        public IReadOnlyList<RentalSummary> Rentals { get; } = rentals;
    }
}
