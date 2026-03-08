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
    /// API controller for vehicle operations (create, list available).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator.</param>
        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new vehicle in the fleet.
        /// </summary>
        /// <param name="request">The create vehicle request.</param>
        /// <returns>201 Created with the vehicle data, or 400 if the vehicle is too old for the fleet.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<VehicleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Gets a vehicle by identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>200 with vehicle data, or 404 if not found.</returns>
        [HttpGet("{id}", Name = "GetVehicle")]
        [ProducesResponseType(typeof(ApiResponse<VehicleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicle(Guid id)
        {
            var request = new GetVehicleRequest { Id = id };
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Lists all vehicles available for rent.
        /// </summary>
        /// <returns>200 OK with the list of available vehicles.</returns>
        [HttpGet("available")]
        [ProducesResponseType(typeof(ApiResponse<ListAvailableVehiclesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAvailable()
        {
            var request = new ListAvailableVehiclesRequest();
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return result;
        }
    }
}
