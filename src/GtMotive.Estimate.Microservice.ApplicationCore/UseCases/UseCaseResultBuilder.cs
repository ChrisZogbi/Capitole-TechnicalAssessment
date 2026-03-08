namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Factory for <see cref="UseCaseResult{T}"/> to avoid static members on generic types (CA1000).
    /// </summary>
    public static class UseCaseResultBuilder
    {
        /// <summary>
        /// Creates a successful result with the given data.
        /// </summary>
        /// <typeparam name="T">Type of the payload.</typeparam>
        /// <param name="data">The result payload.</param>
        /// <returns>A successful result.</returns>
        public static UseCaseResult<T> Success<T>(T data) =>
            new(true, data, null, null);

        /// <summary>
        /// Creates a failure result with the given code and message.
        /// </summary>
        /// <typeparam name="T">Type of the payload (unused; for consistency with success).</typeparam>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>A failure result.</returns>
        public static UseCaseResult<T> Failure<T>(string errorCode, string errorMessage) =>
            new(false, default, errorCode, errorMessage);
    }
}
