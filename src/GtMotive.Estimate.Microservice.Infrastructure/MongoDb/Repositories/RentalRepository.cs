using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IRentalRepository"/>.
    /// </summary>
    public sealed class RentalRepository : IRentalRepository
    {
        private const string CollectionName = "rentals";
        private readonly IMongoCollection<RentalDocument> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        public RentalRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _collection = mongoService.Database.GetCollection<RentalDocument>(CollectionName);
            EnsureIndexes();
        }

        /// <inheritdoc/>
        public async Task Add(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            var doc = ToDocument(rental);
            await _collection.InsertOneAsync(doc).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Rental> GetById(Guid rentalId)
        {
            var doc = await _collection
                .Find(x => x.Id == rentalId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task<Rental> GetActiveByRenter(Guid renterId)
        {
            var doc = await _collection
                .Find(x => x.RenterId == renterId && x.EndDate == null)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task Update(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            var doc = ToDocument(rental);
            await _collection.ReplaceOneAsync(
                x => x.Id == rental.Id,
                doc).ConfigureAwait(false);
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
