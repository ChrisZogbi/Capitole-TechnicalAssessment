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
    /// Uses <see cref="IMongoSessionAccessor"/> so that operations participate in the current request's transaction when one is started.
    /// </summary>
    public sealed class VehicleRepository : IVehicleRepository
    {
        private const string CollectionName = "vehicles";
        private readonly IMongoCollection<VehicleDocument> _collection;
        private readonly IMongoSessionAccessor _sessionAccessor;
        private readonly IAppLogger<VehicleRepository> _appLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        /// <param name="sessionAccessor">The session accessor for the current request (enables transactions).</param>
        /// <param name="appLogger">The application logger.</param>
        public VehicleRepository(MongoService mongoService, IMongoSessionAccessor sessionAccessor, IAppLogger<VehicleRepository> appLogger)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(sessionAccessor);
            ArgumentNullException.ThrowIfNull(appLogger);
            _collection = mongoService.Database.GetCollection<VehicleDocument>(CollectionName);
            _sessionAccessor = sessionAccessor;
            _appLogger = appLogger;
            EnsureIndexes();
        }

        /// <inheritdoc/>
        public async Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            try
            {
                var doc = ToDocument(vehicle);
                await _collection.InsertOneAsync(_sessionAccessor.Session, doc).ConfigureAwait(false);
            }
            catch (MongoException ex)
            {
                _appLogger.LogError(ex, "VehicleRepository.Add failed. VehicleId {VehicleId}", vehicle.Id);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Vehicle> GetById(Guid vehicleId)
        {
            var doc = await _collection
                .Find(_sessionAccessor.Session, x => x.Id == vehicleId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return doc == null ? null : ToEntity(doc);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Vehicle>> GetAvailable()
        {
            var docs = await _collection
                .Find(_sessionAccessor.Session, x => x.IsAvailable)
                .ToListAsync()
                .ConfigureAwait(false);
            var vehicles = docs.ConvertAll(ToEntity);
            return vehicles;
        }

        /// <inheritdoc/>
        public async Task Update(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            try
            {
                var doc = ToDocument(vehicle);
                await _collection.ReplaceOneAsync(
                    _sessionAccessor.Session,
                    x => x.Id == vehicle.Id,
                    doc).ConfigureAwait(false);
            }
            catch (MongoException ex)
            {
                _appLogger.LogError(ex, "VehicleRepository.Update failed. VehicleId {VehicleId}", vehicle.Id);
                throw;
            }
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
