using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for getting a vehicle by id.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetVehicleRequestHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The get vehicle use case.</param>
    public sealed class GetVehicleRequestHandler(IGetVehicleUseCase useCase) : IRequestHandler<GetVehicleRequest, IActionResult>
    {
        private readonly IGetVehicleUseCase _useCase = useCase;

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(GetVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new GetVehicleInput { VehicleId = request.Id };
            var result = await _useCase.Execute(input).ConfigureAwait(false);
            if (!result.IsSuccess || result.Data == null)
            {
                return new NotFoundObjectResult(ApiResponseBuilder.FromError(result.ErrorCode?.ToString() ?? "VehicleNotFound", result.ErrorMessage ?? "The vehicle was not found."));
            }

            var vehicle = result.Data;
            var response = new VehicleResponse(
                vehicle.Id,
                vehicle.ManufacturingDate,
                vehicle.IsAvailable);
            return new OkObjectResult(ApiResponseBuilder.Success(response));
        }
    }
}
