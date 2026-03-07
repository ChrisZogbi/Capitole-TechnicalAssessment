namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Result of the Rent Vehicle use case (success with data or error).
    /// </summary>
    public sealed class RentVehicleResult
    {
        private RentVehicleResult()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; private init; }

        /// <summary>
        /// Gets the output when successful; null when failed.
        /// </summary>
        public RentVehicleOutput Output { get; private init; }

        /// <summary>
        /// Gets the error code when failed (e.g. "NotFound", "VehicleNotAvailable", "RenterAlreadyHasActiveRental").
        /// </summary>
        public string ErrorCode { get; private init; }

        /// <summary>
        /// Gets the error message when failed.
        /// </summary>
        public string ErrorMessage { get; private init; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <param name="output">The rental data.</param>
        /// <returns>A successful result.</returns>
        public static RentVehicleResult Success(RentVehicleOutput output) => new()
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
        public static RentVehicleResult Failure(string errorCode, string message) => new()
        {
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = message,
        };
    }
}
