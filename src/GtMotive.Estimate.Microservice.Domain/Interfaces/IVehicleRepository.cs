using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for vehicle persistence. Implemented by the infrastructure layer.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a vehicle to the fleet.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(Vehicle vehicle);

        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle if found; otherwise null.</returns>
        Task<Vehicle> GetById(Guid vehicleId);

        /// <summary>
        /// Gets all vehicles that are currently available for rent.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailable();

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(Vehicle vehicle);
    }
}
