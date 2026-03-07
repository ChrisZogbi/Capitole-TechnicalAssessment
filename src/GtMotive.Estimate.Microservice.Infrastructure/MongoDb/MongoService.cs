using System;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Provides access to MongoDB client and database.
    /// The connection is created when the service is first resolved from the container (lazy).
    /// </summary>
    public class MongoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoService"/> class.
        /// </summary>
        /// <param name="options">MongoDB settings (connection string and database name), via IOptionsMonitor so configuration can be reloaded.</param>
        public MongoService(IOptionsMonitor<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            var settings = options.CurrentValue;
            ArgumentNullException.ThrowIfNull(settings);

            MongoClient = new MongoClient(settings.ConnectionString);
            var dbName = settings.MongoDbDatabaseName;
            Database = string.IsNullOrWhiteSpace(dbName)
                ? throw new InvalidOperationException("MongoDbDatabaseName must be configured.")
                : MongoClient.GetDatabase(dbName);
        }

        /// <summary>
        /// Gets the MongoDB client.
        /// </summary>
        public MongoClient MongoClient { get; }

        /// <summary>
        /// Gets the MongoDB database for the application.
        /// </summary>
        public IMongoDatabase Database { get; }
    }
}
