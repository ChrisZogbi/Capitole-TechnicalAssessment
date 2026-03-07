using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to return a rented vehicle.
    /// </summary>
    public sealed class ReturnVehicleRequest : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the rental identifier to return.
        /// </summary>
        [Required(ErrorMessage = "Rental ID is required.")]
        [JsonRequired]
        public Guid RentalId { get; set; }
    }
}
