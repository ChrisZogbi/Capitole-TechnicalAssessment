using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for vehicle persistence. Implemented by the infrastructure layer.
    /// Extends the generic repository with vehicle-specific queries.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle, Guid>
    {
        /// <summary>
        /// Gets all vehicles that are currently available for rent.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailable();
    }
}
