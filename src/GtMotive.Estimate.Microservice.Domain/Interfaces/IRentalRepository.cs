using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for rental persistence. Implemented by the infrastructure layer.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Adds a new rental.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(Rental rental);

        /// <summary>
        /// Gets a rental by its identifier.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <returns>The rental if found; otherwise null.</returns>
        Task<Rental> GetById(Guid rentalId);

        /// <summary>
        /// Gets the active rental for a given renter, if any.
        /// Used to enforce the rule: a same person cannot have more than one vehicle rented at a time.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns>The active rental if the renter has one; otherwise null.</returns>
        Task<Rental> GetActiveByRenter(Guid renterId);

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(Rental rental);
    }
}
