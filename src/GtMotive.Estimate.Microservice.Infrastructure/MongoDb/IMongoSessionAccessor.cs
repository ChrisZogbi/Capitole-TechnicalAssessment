using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Provides the current MongoDB session for the request scope.
    /// Repositories use it to participate in the same transaction when <see cref="Domain.Interfaces.IUnitOfWork.BeginTransactionAsync"/> has been called.
    /// </summary>
    public interface IMongoSessionAccessor
    {
        /// <summary>
        /// Gets the current client session. Same instance for the whole request scope.
        /// Operations that use this session will be part of a transaction if one was started via <see cref="Domain.Interfaces.IUnitOfWork.BeginTransactionAsync"/>.
        /// </summary>
        IClientSessionHandle Session { get; }
    }
}
