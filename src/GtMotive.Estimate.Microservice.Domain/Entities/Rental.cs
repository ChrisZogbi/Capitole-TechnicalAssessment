using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate root representing a vehicle rental.
    /// End date is null while the rental is active.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <param name="vehicleId">The rented vehicle identifier.</param>
        /// <param name="renterId">The renter (customer) identifier.</param>
        /// <param name="startDate">The rental start date.</param>
        /// <param name="endDate">The rental end date (null if still active).</param>
        public Rental(Guid id, Guid vehicleId, Guid renterId, DateTime startDate, DateTime? endDate = null)
        {
            Id = id;
            VehicleId = vehicleId;
            RenterId = renterId;
            StartDate = startDate.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(startDate, DateTimeKind.Utc)
                : startDate.ToUniversalTime();

            if (endDate.HasValue)
            {
                var end = endDate.Value;
                EndDate = end.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(end, DateTimeKind.Utc)
                    : end.ToUniversalTime();
            }
            else
            {
                EndDate = null;
            }
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the rented vehicle identifier.
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

        /// <summary>
        /// Gets the rental end date (null if the rental is still active).
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this rental is still active (not yet returned).
        /// </summary>
        public bool IsActive => !EndDate.HasValue;

        /// <summary>
        /// Marks the rental as returned by setting the end date.
        /// </summary>
        /// <param name="endDate">The return date.</param>
        /// <exception cref="InvalidOperationException">Thrown when the rental is already returned.</exception>
        public void MarkAsReturned(DateTime endDate)
        {
            if (EndDate.HasValue)
            {
                throw new InvalidOperationException("The rental has already been returned.");
            }

            EndDate = endDate.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(endDate, DateTimeKind.Utc)
                : endDate.ToUniversalTime();
        }
    }
}
