using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Unit Of Work. Should only be used by Use Cases.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Begins a transaction so that subsequent operations in the same scope participate in it.
        /// Call this before performing multiple writes that must commit or roll back together.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that completes when the transaction has been started.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Applies all database changes (commits the transaction).
        /// </summary>
        /// <returns>Number of affected rows.</returns>
        Task<int> Save();
    }
}
