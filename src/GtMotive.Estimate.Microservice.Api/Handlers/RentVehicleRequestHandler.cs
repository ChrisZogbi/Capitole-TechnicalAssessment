using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for renting a vehicle.
    /// </summary>
    public sealed class RentVehicleRequestHandler : IRequestHandler<RentVehicleRequest, IActionResult>
    {
        private readonly IRentVehicleUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">The rent vehicle use case.</param>
        public RentVehicleRequestHandler(IRentVehicleUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new RentVehicleInput(request.VehicleId, request.RenterId);
            var result = await _useCase.Execute(input).ConfigureAwait(false);

            if (result.IsSuccess && result.Data != null)
            {
                var output = result.Data;
                var response = new RentVehicleResponse(output.RentalId, output.VehicleId, output.RenterId, output.StartDate);
                return new OkObjectResult(ApiResponseBuilder.Success(response));
            }

            var code = result.ErrorCode?.ToString() ?? "Error";
            var message = result.ErrorMessage ?? "An error occurred.";
            return result.ErrorCode switch
            {
                UseCaseErrorCode.NotFound => new NotFoundObjectResult(ApiResponseBuilder.FromError(code, message)),
                UseCaseErrorCode.VehicleNotFound => new NotFoundObjectResult(ApiResponseBuilder.FromError(code, message)),
                UseCaseErrorCode.RentalNotFound => new NotFoundObjectResult(ApiResponseBuilder.FromError(code, message)),
                UseCaseErrorCode.VehicleNotAvailable or UseCaseErrorCode.RenterAlreadyHasActiveRental => new ConflictObjectResult(ApiResponseBuilder.FromError(code, message)),
                UseCaseErrorCode.VehicleTooOldForFleet => new BadRequestObjectResult(ApiResponseBuilder.FromError(code, message)),
                _ => new BadRequestObjectResult(ApiResponseBuilder.FromError(code, message)),
            };
        }
    }
}
