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
    public sealed class ListAvailableVehiclesRequestHandler : IRequestHandler<ListAvailableVehiclesRequest, IActionResult>
    {
        private readonly IListAvailableVehiclesUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesRequestHandler"/> class.
        /// </summary>
        /// <param name="useCase">The list available vehicles use case.</param>
        public ListAvailableVehiclesRequestHandler(IListAvailableVehiclesUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = ListAvailableVehiclesInput.Default;
            var result = await _useCase.Execute(input).ConfigureAwait(false);
            var vehicles = result.Output.Vehicles
                .Select(v => new VehicleResponse(v.Id, v.ManufacturingDate, v.IsAvailable))
                .ToList();
            var response = new ListAvailableVehiclesResponse(vehicles);
            return new OkObjectResult(response);
        }
    }
}
