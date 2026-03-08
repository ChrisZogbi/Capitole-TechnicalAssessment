using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle
{
    /// <summary>
    /// Input for getting a vehicle by identifier.
    /// </summary>
    public sealed class GetVehicleInput
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; set; }
    }
}
