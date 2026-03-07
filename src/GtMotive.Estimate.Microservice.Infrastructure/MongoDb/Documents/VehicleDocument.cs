using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents
{
    /// <summary>
    /// MongoDB document for a vehicle in the fleet.
    /// </summary>
    public class VehicleDocument
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing date (stored as UTC date).
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
