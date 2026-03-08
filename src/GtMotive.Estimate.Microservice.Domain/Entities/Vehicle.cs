using System;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate root representing a vehicle in the fleet.
    /// The fleet cannot contain vehicles with a manufacturing date more than 5 years in the past (enforced by ManufacturingDate).
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </remarks>
    /// <param name="id">The vehicle identifier.</param>
    /// <param name="manufacturingDate">The manufacturing date (validated: not older than 5 years).</param>
    /// <param name="isAvailable">Whether the vehicle is available for rent.</param>
    public class Vehicle(Guid id, ManufacturingDate manufacturingDate, bool isAvailable = true)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid Id { get; } = id;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public ManufacturingDate ManufacturingDate { get; } = manufacturingDate;

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable { get; private set; } = isAvailable;

        /// <summary>
        /// Marks the vehicle as rented (not available).
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the vehicle is already rented.</exception>
        public void MarkAsRented()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("The vehicle is already rented.");
            }

            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as returned (available).
        /// </summary>
        public void MarkAsReturned()
        {
            IsAvailable = true;
        }
    }
}
