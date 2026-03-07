using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to rent a vehicle.
    /// </summary>
    public sealed class RentVehicleRequest : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the vehicle identifier to rent.
        /// </summary>
        [Required(ErrorMessage = "Vehicle ID is required.")]
        [JsonRequired]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the renter (customer) identifier. A renter can only have one active rental at a time.
        /// </summary>
        [Required(ErrorMessage = "Renter ID is required.")]
        [JsonRequired]
        public Guid RenterId { get; set; }
    }
}
