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
    public sealed class GetVehicleRequestHandler : IRequestHandler<GetVehicleRequest, IActionResult>
    {
        private readonly IGetVehicleUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">The get vehicle use case.</param>
        public GetVehicleRequestHandler(IGetVehicleUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(GetVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new GetVehicleInput { VehicleId = request.Id };
            var result = await _useCase.Execute(input).ConfigureAwait(false);
            if (!result.Found)
            {
                return new NotFoundObjectResult(ApiResponseBuilder.FromError("VehicleNotFound", "The vehicle was not found."));
            }

            var response = new VehicleResponse(
                result.Vehicle.Id,
                result.Vehicle.ManufacturingDate,
                result.Vehicle.IsAvailable);
            return new OkObjectResult(ApiResponseBuilder.Success(response));
        }
    }
}
