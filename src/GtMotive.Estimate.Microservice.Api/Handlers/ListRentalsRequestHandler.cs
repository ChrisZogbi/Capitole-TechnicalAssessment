using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Handlers
{
    /// <summary>
    /// MediatR handler for listing rentals with optional active-only filter.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListRentalsRequestHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The list rentals use case.</param>
    public sealed class ListRentalsRequestHandler(IListRentalsUseCase useCase) : IRequestHandler<ListRentalsRequest, IActionResult>
    {
        private readonly IListRentalsUseCase _useCase = useCase;

        /// <inheritdoc/>
        public async Task<IActionResult> Handle(ListRentalsRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var input = new ListRentalsInput(request.ActiveOnly);
            var result = await _useCase.Execute(input).ConfigureAwait(false);
            var data = result.Data;
            if (data == null)
            {
                return new OkObjectResult(ApiResponseBuilder.Success(new ListRentalsResponse([])));
            }

            var items = data.Rentals
                .Select(r => new RentalListItemResponse(r.RentalId, r.VehicleId, r.RenterId, r.StartDate, r.EndDate))
                .ToList();
            var response = new ListRentalsResponse(items);
            return new OkObjectResult(ApiResponseBuilder.Success(response));
        }
    }
}
