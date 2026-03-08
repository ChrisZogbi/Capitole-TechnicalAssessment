using System;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GtMotive.Estimate.Microservice.Api.Filters
{
    /// <summary>Exception filter that maps domain and unhandled exceptions to appropriate HTTP responses (envelope format).</summary>
    /// <param name="appLogger">Logger for exception and domain messages.</param>
    public sealed class BusinessExceptionFilter(IAppLogger<BusinessExceptionFilter> appLogger) : IExceptionFilter
    {
        private readonly IAppLogger<BusinessExceptionFilter> _appLogger = appLogger;

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _appLogger.LogError(context.Exception, "Exception captured in BusinessExceptionFilter.");
            var message = context.Exception.Message;

            if (context.Exception is GtMotive.Estimate.Microservice.Domain.DomainException domainEx)
            {
                var code = domainEx switch
                {
                    VehicleTooOldForFleetException => "VehicleTooOldForFleet",
                    VehicleNotAvailableException => "VehicleNotAvailable",
                    RenterAlreadyHasActiveRentalException => "RenterAlreadyHasActiveRental",
                    _ => "DomainError",
                };

                _appLogger.LogWarning("Domain Exception: {code} - {message}", code, message);
                context.Result = new BadRequestObjectResult(ApiResponseBuilder.FromError(code, message));
                context.Exception = null;
            }
            else
            {
                _appLogger.LogError(context.Exception, "Unhandled Exception");
                context.Result = new InternalServerErrorObjectResult(ApiResponseBuilder.FromError("InternalServerError", message));
                context.Exception = null;
            }
        }
    }
}
