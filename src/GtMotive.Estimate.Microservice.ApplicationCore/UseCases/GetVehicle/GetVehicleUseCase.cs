using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle
{
    /// <summary>
    /// Gets a vehicle by identifier. Returns not found when the vehicle does not exist.
    /// </summary>
    public sealed class GetVehicleUseCase(IVehicleRepository vehicleRepository) : IGetVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        /// <inheritdoc/>
        public async Task<UseCaseResult<VehicleSummary>> Execute(GetVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            var vehicle = await _vehicleRepository.GetById(input.VehicleId).ConfigureAwait(false);
            if (vehicle == null)
            {
                return UseCaseResultBuilder.Failure<VehicleSummary>(UseCaseErrorCode.VehicleNotFound, "The vehicle was not found.");
            }

            var summary = new VehicleSummary(
                vehicle.Id,
                vehicle.ManufacturingDate.Value,
                vehicle.IsAvailable);
            return UseCaseResultBuilder.Success(summary);
        }
    }
}
