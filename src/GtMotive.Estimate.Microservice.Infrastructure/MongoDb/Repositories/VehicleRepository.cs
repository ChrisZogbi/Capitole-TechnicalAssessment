using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB implementation of <see cref="IVehicleRepository"/>.
    /// </summary>
    public sealed class VehicleRepository : IVehicleRepository
    {
        private const string CollectionName = "vehicles";
        private readonly IMongoCollection<VehicleDocument> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        public VehicleRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _collection = mongoService.Database.GetCollection<VehicleDocument>(CollectionName);
            EnsureIndexes();
        }

        /// <inheritdoc/>
        public async Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            var doc = ToDocument(vehicle);
            await _collection.InsertOneAsync(doc).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Vehicle> GetById(Guid vehicleId)
        {
            var doc = await _collection
                .Find(x => x.Id == vehicleId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Vehicle>> GetAvailable()
        {
            var docs = await _collection
                .Find(x => x.IsAvailable)
                .ToListAsync()
                .ConfigureAwait(false);
            var vehicles = docs.ConvertAll(ToEntity);
            return vehicles;
        }

        /// <inheritdoc/>
        public async Task Update(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            var doc = ToDocument(vehicle);
            await _collection.ReplaceOneAsync(
                x => x.Id == vehicle.Id,
                doc).ConfigureAwait(false);
        }

        private static VehicleDocument ToDocument(Vehicle vehicle)
        {
            return new VehicleDocument
            {
                Id = vehicle.Id,
                ManufacturingDate = vehicle.ManufacturingDate.Value,
                IsAvailable = vehicle.IsAvailable,
            };
        }

        private static Vehicle ToEntity(VehicleDocument doc)
        {
            var manufacturingDate = new ManufacturingDate(doc.ManufacturingDate);
            return new Vehicle(doc.Id, manufacturingDate, doc.IsAvailable);
        }

        private void EnsureIndexes()
        {
            try
            {
                var indexKeys = Builders<VehicleDocument>.IndexKeys.Ascending(x => x.IsAvailable);
                _collection.Indexes.CreateOne(new CreateIndexModel<VehicleDocument>(indexKeys));
            }
            catch (MongoCommandException)
            {
                // Index may already exist (e.g. duplicate key error); ignore for idempotency.
            }
        }
    }
}
