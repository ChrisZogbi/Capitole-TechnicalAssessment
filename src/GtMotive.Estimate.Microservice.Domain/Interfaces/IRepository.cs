using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Generic port for entity persistence (add, get by id, update).
    /// Implemented by the infrastructure layer. Specific repositories extend this with additional methods.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TId">The type of the entity identifier.</typeparam>
    public interface IRepository<TEntity, in TId>
    {
        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(TEntity entity);

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The entity if found; otherwise null.</returns>
        Task<TEntity> GetById(TId id);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(TEntity entity);
    }
}
