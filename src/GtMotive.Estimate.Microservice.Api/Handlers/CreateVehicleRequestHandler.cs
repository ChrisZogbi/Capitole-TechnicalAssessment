using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for creating a vehicle.
    /// </summary>
    public sealed class CreateVehicleRequestHandler : IRequestHandler<CreateVehicleRequest, IActionResult>
    {
        private readonly ICreateVehicleUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">The create vehicle use case.</param>
        public CreateVehicleRequestHandler(ICreateVehicleUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new CreateVehicleInput(request.ManufacturingDate);
            var result = await _useCase.Execute(input).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                var output = result.Output;
                var response = new VehicleResponse(output.VehicleId, output.ManufacturingDate, output.IsAvailable);
                return new CreatedAtRouteResult(
                    "GetVehicle",
                    new { id = output.VehicleId },
                    ApiResponseBuilder.Success(response));
            }

            return new BadRequestObjectResult(ApiResponseBuilder.FromError("VehicleTooOldForFleet", result.ErrorMessage ?? "The fleet cannot contain vehicles with a manufacturing date older than 5 years."));
        }
    }
}
