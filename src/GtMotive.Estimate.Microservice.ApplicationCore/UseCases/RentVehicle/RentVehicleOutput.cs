using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output after successfully renting a vehicle.
    /// </summary>
    public sealed class RentVehicleOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="renterId">The renter identifier.</param>
        /// <param name="startDate">The rental start date.</param>
        public RentVehicleOutput(Guid rentalId, Guid vehicleId, Guid renterId, DateTime startDate)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            RenterId = renterId;
            StartDate = startDate;
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
        /// Gets the renter identifier.
        /// </summary>
        public Guid RenterId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }
    }
}
