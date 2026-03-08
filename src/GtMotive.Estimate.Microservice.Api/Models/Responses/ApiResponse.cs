#nullable enable

using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Homogeneous API response envelope. All endpoints return this structure.
    /// </summary>
    /// <typeparam name="T">Type of the success payload (data).</typeparam>
    public sealed class ApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        /// <param name="isSuccess">Whether the operation succeeded.</param>
        /// <param name="data">The payload on success; null on failure.</param>
        /// <param name="error">The error details on failure; null on success.</param>
        public ApiResponse(bool isSuccess, T? data, ApiError? error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets the payload on success; null when <see cref="IsSuccess"/> is false.
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; }

        /// <summary>
        /// Gets the error details on failure; null when <see cref="IsSuccess"/> is true.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApiError? Error { get; }
    }
}
