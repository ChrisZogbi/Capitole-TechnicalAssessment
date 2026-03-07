using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Response after successfully renting a vehicle.
    /// </summary>
    public sealed class RentVehicleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleResponse"/> class.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="renterId">The renter identifier.</param>
        /// <param name="startDate">The rental start date.</param>
        public RentVehicleResponse(Guid rentalId, Guid vehicleId, Guid renterId, DateTime startDate)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            RenterId = renterId;
            StartDate = startDate;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        [Required]
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        [Required]
        public Guid RenterId { get; }

        /// <summary>
        /// Gets the rental start date (UTC).
        /// </summary>
        [Required]
        public DateTime StartDate { get; }
    }
}
