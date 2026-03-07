using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// MongoDB implementation of <see cref="IUnitOfWork"/> using a client session and multi-document transactions.
    /// One instance per request (Scoped). Repositories use <see cref="IMongoSessionAccessor"/> to get the same session.
    /// </summary>
    /// <remarks>
    /// MongoDB multi-document transactions require a replica set (even a single-node replica set for local development).
    /// </remarks>
    public sealed class MongoDbUnitOfWork : IUnitOfWork, IMongoSessionAccessor, IDisposable
    {
        private readonly IClientSessionHandle _session;
        private bool _transactionStarted;
        private bool _committed;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbUnitOfWork"/> class.
        /// Starts a session (no transaction yet). Transaction is started when <see cref="BeginTransactionAsync"/> is called.
        /// </summary>
        /// <param name="mongoService">The MongoDB service (client and database).</param>
        public MongoDbUnitOfWork(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _session = mongoService.MongoClient.StartSession();
        }

        /// <inheritdoc />
        public IClientSessionHandle Session => _session;

        /// <inheritdoc />
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_transactionStarted)
            {
                return;
            }

            _session.StartTransaction();
            _transactionStarted = true;
            await Task.CompletedTask.ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<int> Save()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (!_transactionStarted)
            {
                return 0;
            }

            await _session.CommitTransactionAsync(cancellationToken: default).ConfigureAwait(false);
            _committed = true;
            return 1;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                if (_transactionStarted && !_committed)
                {
                    _session.AbortTransaction();
                }
            }
            finally
            {
                _session?.Dispose();
                _disposed = true;
            }
        }
    }
}
