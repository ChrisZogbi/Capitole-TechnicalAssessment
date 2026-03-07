using System;
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
    }
}
