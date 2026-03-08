#nullable enable

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Generic result of a use case: success with typed data or failure with error code and message.
    /// </summary>
    /// <typeparam name="T">Type of the payload on success.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UseCaseResult{T}"/> class.
    /// </remarks>
    /// <param name="isSuccess">Whether the operation succeeded.</param>
    /// <param name="data">The payload on success; null on failure.</param>
    /// <param name="errorCode">Error code on failure; null on success.</param>
    /// <param name="errorMessage">Error message on failure; null on success.</param>
    public sealed class UseCaseResult<T>(bool isSuccess, T? data, UseCaseErrorCode? errorCode, string? errorMessage)
    {
        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; } = isSuccess;

        /// <summary>
        /// Gets the payload on success; null when <see cref="IsSuccess"/> is false.
        /// </summary>
        public T? Data { get; } = data;

        /// <summary>
        /// Gets the error code on failure; null when <see cref="IsSuccess"/> is true.
        /// </summary>
        public UseCaseErrorCode? ErrorCode { get; } = errorCode;

        /// <summary>
        /// Gets the error message on failure; null when <see cref="IsSuccess"/> is true.
        /// </summary>
        public string? ErrorMessage { get; } = errorMessage;
    }
}
