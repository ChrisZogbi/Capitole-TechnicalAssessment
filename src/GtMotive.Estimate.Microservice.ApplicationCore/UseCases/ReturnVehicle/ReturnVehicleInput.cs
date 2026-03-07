using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input for returning a rented vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleInput"/> class.
    /// </remarks>
    /// <param name="rentalId">The rental to return.</param>
    public sealed class ReturnVehicleInput(Guid rentalId)
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; } = rentalId;
    }
}
