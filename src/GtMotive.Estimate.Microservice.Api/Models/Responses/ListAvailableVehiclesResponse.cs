using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Models.Responses
{
    /// <summary>
    /// Response containing the list of vehicles available for rent.
    /// </summary>
    public sealed class ListAvailableVehiclesResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesResponse"/> class.
        /// </summary>
        /// <param name="vehicles">The list of available vehicles.</param>
        public ListAvailableVehiclesResponse(IReadOnlyList<VehicleResponse> vehicles)
        {
            Vehicles = vehicles;
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        [Required]
        public IReadOnlyList<VehicleResponse> Vehicles { get; }
    }
}
