namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Error payload inside the API response envelope.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ApiError"/> class.
    /// </remarks>
    /// <param name="code">Error code (e.g. VehicleTooOldForFleet, NotFound, ValidationError).</param>
    /// <param name="message">Human-readable error message.</param>
    public sealed class ApiError(string code, string message)
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Code { get; } = code;

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; } = message;
    }
}
