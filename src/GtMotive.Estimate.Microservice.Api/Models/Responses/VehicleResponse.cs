using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Vehicle data in API responses.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VehicleResponse"/> class.
    /// </remarks>
    /// <param name="id">The vehicle identifier.</param>
    /// <param name="manufacturingDate">The manufacturing date.</param>
    /// <param name="isAvailable">Whether the vehicle is available for rent.</param>
    public sealed class VehicleResponse(Guid id, DateTime manufacturingDate, bool isAvailable)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        [Required]
        public Guid Id { get; } = id;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        [Required]
        public DateTime ManufacturingDate { get; } = manufacturingDate;

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        [Required]
        public bool IsAvailable { get; } = isAvailable;
    }
}
