using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for listing available vehicles.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListAvailableVehiclesRequestHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The list available vehicles use case.</param>
    public sealed class ListAvailableVehiclesRequestHandler(IListAvailableVehiclesUseCase useCase) : IRequestHandler<ListAvailableVehiclesRequest, IActionResult>
    {
        private readonly IListAvailableVehiclesUseCase _useCase = useCase;

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = ListAvailableVehiclesInput.Default;
            var result = await _useCase.Execute(input).ConfigureAwait(false);
            var data = result.Data;
            if (data == null)
            {
                return new OkObjectResult(ApiResponseBuilder.Success(new ListAvailableVehiclesResponse([])));
            }

            var vehicles = data.Vehicles
                .Select(v => new VehicleResponse(v.Id, v.ManufacturingDate, v.IsAvailable))
                .ToList();
            var response = new ListAvailableVehiclesResponse(vehicles);
            return new OkObjectResult(ApiResponseBuilder.Success(response));
        }
    }
}
