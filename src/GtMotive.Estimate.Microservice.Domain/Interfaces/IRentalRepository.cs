using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for rental persistence. Implemented by the infrastructure layer.
    /// Extends the generic repository with rental-specific queries.
    /// </summary>
    public interface IRentalRepository : IRepository<Rental, Guid>
    {
        /// <summary>
        /// Gets the active rental for a given renter, if any.
        /// Used to enforce the rule: a same person cannot have more than one vehicle rented at a time.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns>The active rental if the renter has one; otherwise null.</returns>
        Task<Rental> GetActiveByRenter(Guid renterId);

        /// <summary>
        /// Gets rentals optionally filtered by active state.
        /// </summary>
        /// <param name="activeOnly">True = only active (EndDate == null), false = only returned (EndDate != null), null = all.</param>
        /// <returns>The list of rentals matching the filter.</returns>
        Task<IReadOnlyList<Rental>> GetRentals(bool? activeOnly);
    }
}
