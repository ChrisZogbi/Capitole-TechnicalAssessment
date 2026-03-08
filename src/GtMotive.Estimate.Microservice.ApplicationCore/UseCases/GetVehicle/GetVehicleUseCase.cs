using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle
{
    /// <summary>
    /// Gets a vehicle by identifier. Returns not found when the vehicle does not exist.
    /// </summary>
    public sealed class GetVehicleUseCase(IVehicleRepository vehicleRepository, IAppLogger<GetVehicleUseCase> appLogger) : IGetVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IAppLogger<GetVehicleUseCase> _appLogger = appLogger;

        /// <inheritdoc/>
        public async Task<UseCaseResult<VehicleSummary>> Execute(GetVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            _appLogger.LogInformation("GetVehicle: starting. VehicleId {VehicleId}", input.VehicleId);
            var vehicle = await _vehicleRepository.GetById(input.VehicleId).ConfigureAwait(false);
            if (vehicle == null)
            {
                _appLogger.LogWarning("GetVehicle: vehicle not found. VehicleId {VehicleId}", input.VehicleId);
                return UseCaseResultBuilder.Failure<VehicleSummary>(UseCaseErrorCode.VehicleNotFound, "The vehicle was not found.");
            }

            _appLogger.LogInformation("GetVehicle: completed. VehicleId {VehicleId}", vehicle.Id);
            var summary = new VehicleSummary(
                vehicle.Id,
                vehicle.ManufacturingDate.Value,
                vehicle.IsAvailable);
            return UseCaseResultBuilder.Success(summary);
        }
    }
}
