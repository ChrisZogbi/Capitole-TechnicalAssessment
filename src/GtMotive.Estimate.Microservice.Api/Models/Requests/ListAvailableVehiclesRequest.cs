using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Models.Requests
{
    /// <summary>
    /// Request to list all vehicles available for rent (no parameters).
    /// </summary>
    public sealed class ListAvailableVehiclesRequest : IRequest<IActionResult>
    {
    }
}
