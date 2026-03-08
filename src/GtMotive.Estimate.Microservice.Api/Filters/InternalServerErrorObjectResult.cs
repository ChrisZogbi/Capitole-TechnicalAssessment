using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GtMotive.Estimate.Microservice.Api.Filters
{
    /// <summary>Object result that returns HTTP 500 Internal Server Error.</summary>
    [ExcludeFromCodeCoverage]
    public class InternalServerErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        /// <summary>Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class with an error object (e.g. ProblemDetails).</summary>
        /// <param name="error">The error value to serialize.</param>
        public InternalServerErrorObjectResult([ActionResultObjectValue] object error)
            : base(error)
        {
            StatusCode = DefaultStatusCode;
        }

        /// <summary>Initializes a new instance of the <see cref="InternalServerErrorObjectResult"/> class with model state errors.</summary>
        /// <param name="modelState">The model state dictionary.</param>
        public InternalServerErrorObjectResult([ActionResultObjectValue] ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
            ArgumentNullException.ThrowIfNull(modelState);

            StatusCode = DefaultStatusCode;
        }
    }
}
