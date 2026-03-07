using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
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

            if (result.IsSuccess)
            {
                var output = result.Output;
                var response = new RentVehicleResponse(output.RentalId, output.VehicleId, output.RenterId, output.StartDate);
                return new OkObjectResult(response);
            }

            return result.ErrorCode switch
            {
                "NotFound" => new NotFoundObjectResult(new { message = result.ErrorMessage }),
                "VehicleNotAvailable" or "RenterAlreadyHasActiveRental" => new ConflictObjectResult(new { message = result.ErrorMessage }),
                _ => new BadRequestObjectResult(new { message = result.ErrorMessage }),
            };
        }
    }
}
