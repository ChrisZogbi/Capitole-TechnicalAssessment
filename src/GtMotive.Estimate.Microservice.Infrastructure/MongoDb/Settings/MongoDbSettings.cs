namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings
{
    /// <summary>MongoDB connection and database settings.</summary>
    public class MongoDbSettings
    {
        /// <summary>Gets or sets the MongoDB connection string.</summary>
        public string ConnectionString { get; set; }

        /// <summary>Gets or sets the database name.</summary>
        public string MongoDbDatabaseName { get; set; }
    }
}
