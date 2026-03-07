using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to create a new vehicle in the fleet.
    /// </summary>
    public sealed class CreateVehicleRequest : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the vehicle manufacturing date. The fleet cannot contain vehicles older than 5 years.
        /// </summary>
        [Required(ErrorMessage = "Manufacturing date is required.")]
        [JsonRequired]
        public DateTime ManufacturingDate { get; set; }
    }
}
