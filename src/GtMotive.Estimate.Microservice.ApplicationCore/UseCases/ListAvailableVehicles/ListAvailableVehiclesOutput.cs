using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Output for the list of available vehicles.
    /// </summary>
    public sealed class ListAvailableVehiclesOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicles">The list of available vehicles.</param>
        public ListAvailableVehiclesOutput(IReadOnlyList<VehicleSummary> vehicles)
        {
            Vehicles = vehicles;
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public IReadOnlyList<VehicleSummary> Vehicles { get; }
    }
}
