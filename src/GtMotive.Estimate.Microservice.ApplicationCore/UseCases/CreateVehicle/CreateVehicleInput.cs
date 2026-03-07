using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input for creating a new vehicle in the fleet.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleInput"/> class.
    /// </remarks>
    /// <param name="manufacturingDate">The vehicle manufacturing date.</param>
    public sealed class CreateVehicleInput(DateTime manufacturingDate)
    {
        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
