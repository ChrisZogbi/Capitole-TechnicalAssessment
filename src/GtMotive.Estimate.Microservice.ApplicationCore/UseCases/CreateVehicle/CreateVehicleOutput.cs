using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output after successfully creating a vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleOutput"/> class.
    /// </remarks>
    /// <param name="vehicleId">The created vehicle identifier.</param>
    /// <param name="manufacturingDate">The manufacturing date.</param>
    /// <param name="isAvailable">Whether the vehicle is available.</param>
    public sealed class CreateVehicleOutput(Guid vehicleId, DateTime manufacturingDate, bool isAvailable)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable { get; } = isAvailable;
    }
}
