using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for returning a rented vehicle.
    /// </summary>
    public sealed class ReturnVehicleRequestHandler : IRequestHandler<ReturnVehicleRequest, IActionResult>
    {
        private readonly IReturnVehicleUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">The return vehicle use case.</param>
        public ReturnVehicleRequestHandler(IReturnVehicleUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new ReturnVehicleInput(request.RentalId);
            var result = await _useCase.Execute(input).ConfigureAwait(false);

            if (result.IsSuccess && result.Data != null)
            {
                var output = result.Data;
                var response = new ReturnVehicleResponse(output.RentalId, output.VehicleId, output.EndDate);
                return new OkObjectResult(ApiResponseBuilder.Success(response));
            }

            return new NotFoundObjectResult(ApiResponseBuilder.FromError(result.ErrorCode ?? "RentalNotFound", result.ErrorMessage ?? "The rental was not found or has already been returned."));
        }
    }
}
