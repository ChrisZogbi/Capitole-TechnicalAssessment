using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents
{
    /// <summary>
    /// MongoDB document for a vehicle rental.
    /// </summary>
    public class RentalDocument
    {
        /// <summary>
        /// Gets or sets the rental identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the rented vehicle identifier.
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the renter identifier.
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid RenterId { get; set; }

        /// <summary>
        /// Gets or sets the rental start date (UTC).
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the rental end date (UTC); null if the rental is still active.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
