namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Result of the List Available Vehicles use case (always success with data).
    /// </summary>
    public sealed class ListAvailableVehiclesResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesResult"/> class.
        /// </summary>
        /// <param name="output">The list of available vehicles.</param>
        public ListAvailableVehiclesResult(ListAvailableVehiclesOutput output)
        {
            Output = output;
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public ListAvailableVehiclesOutput Output { get; }
    }
}
