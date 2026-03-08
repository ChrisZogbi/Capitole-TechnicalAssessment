using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output after successfully renting a vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="renterId">The renter identifier.</param>
    /// <param name="startDate">The rental start date.</param>
    public sealed class RentVehicleOutput(Guid rentalId, Guid vehicleId, Guid renterId, DateTime startDate)
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
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; } = startDate;
    }
}
