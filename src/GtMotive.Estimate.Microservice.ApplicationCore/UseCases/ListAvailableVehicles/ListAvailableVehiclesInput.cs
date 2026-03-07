namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Input for listing available vehicles (no parameters required).
    /// </summary>
    public sealed class ListAvailableVehiclesInput
    {
        /// <summary>
        /// Gets the default instance for parameterless listing.
        /// </summary>
        public static ListAvailableVehiclesInput Default { get; } = new();
    }
}
