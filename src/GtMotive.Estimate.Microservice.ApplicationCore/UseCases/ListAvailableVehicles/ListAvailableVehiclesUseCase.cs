using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Lists all vehicles that are currently available for rent.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
    /// </remarks>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    public sealed class ListAvailableVehiclesUseCase(IVehicleRepository vehicleRepository) : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        /// <inheritdoc/>
        public async Task<ListAvailableVehiclesResult> Execute(ListAvailableVehiclesInput input)
        {
            var vehicles = await _vehicleRepository.GetAvailable().ConfigureAwait(false);
            var summaries = vehicles
                .Select(v => new VehicleSummary(v.Id, v.ManufacturingDate.Value, v.IsAvailable))
                .ToList();
            var output = new ListAvailableVehiclesOutput(summaries);
            return await Task.FromResult(new ListAvailableVehiclesResult(output)).ConfigureAwait(false);
        }
    }
}
