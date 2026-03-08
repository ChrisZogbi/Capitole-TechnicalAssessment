using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input for renting a vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
    /// </remarks>
    /// <param name="vehicleId">The vehicle to rent.</param>
    /// <param name="renterId">The person renting the vehicle.</param>
    public sealed class RentVehicleInput(Guid vehicleId, Guid renterId)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        public Guid RenterId { get; } = renterId;
    }
}
