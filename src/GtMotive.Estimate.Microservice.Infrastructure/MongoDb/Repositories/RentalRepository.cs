using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IRentalRepository"/>.
    /// Uses <see cref="IMongoSessionAccessor"/> so that operations participate in the current request's transaction when one is started.
    /// </summary>
    public sealed class RentalRepository : IRentalRepository
    {
        private const string CollectionName = "rentals";
        private readonly IMongoCollection<RentalDocument> _collection;
        private readonly IMongoSessionAccessor _sessionAccessor;
        private readonly IAppLogger<RentalRepository> _appLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        /// <param name="sessionAccessor">The session accessor for the current request (enables transactions).</param>
        /// <param name="appLogger">The application logger.</param>
        public RentalRepository(MongoService mongoService, IMongoSessionAccessor sessionAccessor, IAppLogger<RentalRepository> appLogger)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(sessionAccessor);
            ArgumentNullException.ThrowIfNull(appLogger);
            _collection = mongoService.Database.GetCollection<RentalDocument>(CollectionName);
            _sessionAccessor = sessionAccessor;
            _appLogger = appLogger;
            EnsureIndexes();
        }

        /// <inheritdoc/>
        public async Task Add(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            try
            {
                var doc = ToDocument(rental);
                await _collection.InsertOneAsync(_sessionAccessor.Session, doc).ConfigureAwait(false);
            }
            catch (MongoException ex)
            {
                _appLogger.LogError(ex, "RentalRepository.Add failed. RentalId {RentalId}", rental.Id);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Rental> GetById(Guid rentalId)
        {
            var doc = await _collection
                .Find(_sessionAccessor.Session, x => x.Id == rentalId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task<Rental> GetActiveByRenter(Guid renterId)
        {
            var doc = await _collection
                .Find(_sessionAccessor.Session, x => x.RenterId == renterId && x.EndDate == null)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Rental>> GetRentals(bool? activeOnly)
        {
            var filter = activeOnly switch
            {
                true => Builders<RentalDocument>.Filter.Eq(x => x.EndDate, null),
                false => Builders<RentalDocument>.Filter.Ne(x => x.EndDate, null),
                _ => Builders<RentalDocument>.Filter.Empty,
            };

            var docs = await _collection
                .Find(_sessionAccessor.Session, filter)
                .ToListAsync()
                .ConfigureAwait(false);
            return docs.ConvertAll(ToEntity);
        }

        /// <inheritdoc/>
        public async Task Update(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            try
            {
                var doc = ToDocument(rental);
                await _collection.ReplaceOneAsync(
                    _sessionAccessor.Session,
                    x => x.Id == rental.Id,
                    doc).ConfigureAwait(false);
            }
            catch (MongoException ex)
            {
                _appLogger.LogError(ex, "RentalRepository.Update failed. RentalId {RentalId}", rental.Id);
                throw;
            }
        }

        private static RentalDocument ToDocument(Rental rental)
        {
            return new RentalDocument
            {
                Id = rental.Id,
                VehicleId = rental.VehicleId,
                RenterId = rental.RenterId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
            };
        }

        private static Rental ToEntity(RentalDocument doc)
        {
            return new Rental(doc.Id, doc.VehicleId, doc.RenterId, doc.StartDate, doc.EndDate);
        }

        private void EnsureIndexes()
        {
            try
            {
                var indexKeys = Builders<RentalDocument>.IndexKeys
                    .Ascending(x => x.RenterId)
                    .Ascending(x => x.EndDate);
                _collection.Indexes.CreateOne(new CreateIndexModel<RentalDocument>(indexKeys));
            }
            catch (MongoCommandException)
            {
                // Index may already exist (e.g. duplicate key error); ignore for idempotency.
            }
        }
    }
}
