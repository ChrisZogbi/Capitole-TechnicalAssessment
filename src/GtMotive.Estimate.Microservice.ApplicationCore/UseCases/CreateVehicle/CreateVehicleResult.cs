namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Result of the Create Vehicle use case (success with data or error).
    /// </summary>
    public sealed class CreateVehicleResult
    {
        private CreateVehicleResult()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; private init; }

        /// <summary>
        /// Gets the output when successful; null when failed.
        /// </summary>
        public CreateVehicleOutput Output { get; private init; }

        /// <summary>
        /// Gets the error code when failed (e.g. "VehicleTooOldForFleet").
        /// </summary>
        public string ErrorCode { get; private init; }

        /// <summary>
        /// Gets the error message when failed.
        /// </summary>
        public string ErrorMessage { get; private init; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <param name="output">The created vehicle data.</param>
        /// <returns>A successful result.</returns>
        public static CreateVehicleResult Success(CreateVehicleOutput output) => new()
        {
            IsSuccess = true,
            Output = output,
        };

        /// <summary>
        /// Creates a failure result.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <returns>A failure result.</returns>
        public static CreateVehicleResult Failure(string errorCode, string message) => new()
        {
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = message,
        };
    }
}
