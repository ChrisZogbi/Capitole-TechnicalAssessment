using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle
{
    /// <summary>
    /// Result of getting a vehicle by id (found or not).
    /// </summary>
    public sealed class GetVehicleResult
    {
        /// <summary>
        /// Gets a value indicating whether the vehicle was found.
        /// </summary>
        public bool Found { get; private init; }

        /// <summary>
        /// Gets the vehicle summary when found; otherwise null.
        /// </summary>
        public VehicleSummary Vehicle { get; private init; }

        /// <summary>
        /// Creates a success result with the vehicle data.
        /// </summary>
        public static GetVehicleResult Success(VehicleSummary vehicle) => new()
        {
            Found = true,
            Vehicle = vehicle,
        };

        /// <summary>
        /// Creates a not-found result.
        /// </summary>
        public static GetVehicleResult NotFound() => new()
        {
            Found = false,
            Vehicle = null,
        };
    }
}
