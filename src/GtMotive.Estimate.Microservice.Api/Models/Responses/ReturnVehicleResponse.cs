using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Response after successfully returning a vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleResponse"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="endDate">The return (end) date.</param>
    public sealed class ReturnVehicleResponse(Guid rentalId, Guid vehicleId, DateTime endDate)
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
        /// Gets the return (end) date (UTC).
        /// </summary>
        [Required]
        public DateTime EndDate { get; } = endDate;
    }
}
