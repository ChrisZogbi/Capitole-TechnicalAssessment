using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// A single rental item in a list response.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentalListItemResponse"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="renterId">The renter identifier.</param>
    /// <param name="startDate">The rental start date.</param>
    /// <param name="endDate">The rental end date (null if still active).</param>
    public sealed class RentalListItemResponse(Guid rentalId, Guid vehicleId, Guid renterId, DateTime startDate, DateTime? endDate)
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        [Required]
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        [Required]
        public Guid RenterId { get; } = renterId;

        /// <summary>
        /// Gets the rental start date (UTC).
        /// </summary>
        [Required]
        public DateTime StartDate { get; } = startDate;

        /// <summary>
        /// Gets the rental end date (UTC); null if the rental is still active.
        /// </summary>
        public DateTime? EndDate { get; } = endDate;
    }
}
