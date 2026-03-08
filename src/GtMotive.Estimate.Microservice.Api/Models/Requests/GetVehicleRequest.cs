using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to get a vehicle by identifier.
    /// </summary>
    public sealed class GetVehicleRequest : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
