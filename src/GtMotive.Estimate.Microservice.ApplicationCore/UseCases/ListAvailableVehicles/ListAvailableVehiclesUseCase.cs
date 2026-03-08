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
    /// <param name="appLogger">The application logger.</param>
    public sealed class ListAvailableVehiclesUseCase(IVehicleRepository vehicleRepository, IAppLogger<ListAvailableVehiclesUseCase> appLogger) : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IAppLogger<ListAvailableVehiclesUseCase> _appLogger = appLogger;

        /// <inheritdoc/>
        public async Task<UseCaseResult<ListAvailableVehiclesOutput>> Execute(ListAvailableVehiclesInput input)
        {
            _appLogger.LogInformation("ListAvailableVehicles: starting");
            var vehicles = await _vehicleRepository.GetAvailable().ConfigureAwait(false);
            var summaries = vehicles
                .Select(v => new VehicleSummary(v.Id, v.ManufacturingDate.Value, v.IsAvailable))
                .ToList();
            var output = new ListAvailableVehiclesOutput(summaries);
            _appLogger.LogInformation("ListAvailableVehicles: completed. Count {Count}", summaries.Count);
            return await Task.FromResult(UseCaseResultBuilder.Success(output)).ConfigureAwait(false);
        }
    }
}
