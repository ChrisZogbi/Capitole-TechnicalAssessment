using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output after successfully returning a vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="endDate">The return (end) date.</param>
    public sealed class ReturnVehicleOutput(Guid rentalId, Guid vehicleId, DateTime endDate)
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
        /// Gets the return (end) date.
        /// </summary>
        public DateTime EndDate { get; } = endDate;
    }
}
