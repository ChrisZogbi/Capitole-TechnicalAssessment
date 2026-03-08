namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Error payload inside the API response envelope.
    /// </summary>
    public sealed class ApiError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="code">Error code (e.g. VehicleTooOldForFleet, NotFound, ValidationError).</param>
        /// <param name="message">Human-readable error message.</param>
        public ApiError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }
    }
}
