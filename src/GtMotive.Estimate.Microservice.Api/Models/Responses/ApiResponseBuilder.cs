namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Centralized builder for API response envelopes.
    /// </summary>
    public static class ApiResponseBuilder
    {
        /// <summary>
        /// Builds a success response with the given data.
        /// </summary>
        /// <typeparam name="T">Type of the payload.</typeparam>
        /// <param name="data">The response payload.</param>
        /// <returns>An envelope with isSuccess true, data set, error null.</returns>
        public static ApiResponse<T> Success<T>(T data) =>
            new(true, data, null);

        /// <summary>
        /// Builds an error response with the given code and message.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <returns>An envelope with isSuccess false, data null, error set.</returns>
        public static ApiResponse<object> FromError(string code, string message) =>
            new(false, null, new ApiError(code, message));
    }
}
