using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals
{
    /// <summary>
    /// Summary of a rental for listing.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentalSummary"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="renterId">The renter identifier.</param>
    /// <param name="startDate">The rental start date.</param>
    /// <param name="endDate">The rental end date (null if still active).</param>
    public sealed class RentalSummary(Guid rentalId, Guid vehicleId, Guid renterId, DateTime startDate, DateTime? endDate)
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        public Guid RenterId { get; } = renterId;

        /// <summary>
        /// Gets the rental start date (UTC).
        /// </summary>
        public DateTime StartDate { get; } = startDate;

        /// <summary>
        /// Gets the rental end date (UTC); null if the rental is still active.
        /// </summary>
        public DateTime? EndDate { get; } = endDate;
    }
}
