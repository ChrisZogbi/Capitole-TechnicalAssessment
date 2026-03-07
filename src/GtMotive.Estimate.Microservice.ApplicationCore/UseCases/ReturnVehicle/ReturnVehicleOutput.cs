using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output after successfully returning a vehicle.
    /// </summary>
    public sealed class ReturnVehicleOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="endDate">The return (end) date.</param>
        public ReturnVehicleOutput(Guid rentalId, Guid vehicleId, DateTime endDate)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the return (end) date.
        /// </summary>
        public DateTime EndDate { get; }
    }
}
