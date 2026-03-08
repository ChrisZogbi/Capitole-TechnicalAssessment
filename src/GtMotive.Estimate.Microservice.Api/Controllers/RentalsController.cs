using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// API controller for rental operations (rent, return).
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentalsController"/> class.
    /// </remarks>
    /// <param name="mediator">The MediatR mediator.</param>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class RentalsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Lists rentals with optional filter by active state.
        /// </summary>
        /// <param name="activeOnly">Optional: true = only active rentals, false = only returned, omit = all.</param>
        /// <returns>200 OK with the list of rentals in envelope format.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ListRentalsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List([FromQuery] bool? activeOnly)
        {
            var request = new ListRentalsRequest { ActiveOnly = activeOnly };
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Rents a vehicle for a renter.
        /// </summary>
        /// <param name="request">The rent vehicle request (vehicleId, renterId).</param>
        /// <returns>200 OK with rental data, 404 if vehicle not found, 409 if vehicle not available or renter already has a rental.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<RentVehicleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Rent([FromBody] RentVehicleRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>200 OK with return data, or 404 if rental not found or already returned.</returns>
        [HttpPost("{id}/return")]
        [ProducesResponseType(typeof(ApiResponse<ReturnVehicleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Return(Guid id)
        {
            var request = new ReturnVehicleRequest { RentalId = id };
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }
    }
}
