using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Output for the list of available vehicles.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListAvailableVehiclesOutput"/> class.
    /// </remarks>
    /// <param name="vehicles">The list of available vehicles.</param>
    public sealed class ListAvailableVehiclesOutput(IReadOnlyList<VehicleSummary> vehicles)
    {
        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public IReadOnlyList<VehicleSummary> Vehicles { get; } = vehicles;
    }
}
