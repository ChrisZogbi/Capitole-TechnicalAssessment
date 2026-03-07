using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Summary of a vehicle for listing.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VehicleSummary"/> class.
    /// </remarks>
    /// <param name="id">The vehicle identifier.</param>
    /// <param name="manufacturingDate">The manufacturing date.</param>
    /// <param name="isAvailable">Whether the vehicle is available.</param>
    public sealed class VehicleSummary(Guid id, DateTime manufacturingDate, bool isAvailable)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid Id { get; } = id;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;

        /// <summary>
        /// Gets a value indicating whether the vehicle is available.
        /// </summary>
        public bool IsAvailable { get; } = isAvailable;
    }
}
