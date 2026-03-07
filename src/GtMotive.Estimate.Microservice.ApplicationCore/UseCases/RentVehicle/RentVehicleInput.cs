using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input for renting a vehicle.
    /// </summary>
    public sealed class RentVehicleInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle to rent.</param>
        /// <param name="renterId">The person renting the vehicle.</param>
        public RentVehicleInput(Guid vehicleId, Guid renterId)
        {
            VehicleId = vehicleId;
            RenterId = renterId;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        public Guid RenterId { get; }
    }
}
