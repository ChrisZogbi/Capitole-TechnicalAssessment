using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Creates a new vehicle in the fleet. The manufacturing date must not be older than 5 years (enforced by domain).
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
    /// </remarks>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="appLogger">The application logger.</param>
    public sealed class CreateVehicleUseCase(IVehicleRepository vehicleRepository, IAppLogger<CreateVehicleUseCase> appLogger) : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IAppLogger<CreateVehicleUseCase> _appLogger = appLogger;

        /// <inheritdoc/>
        public async Task<UseCaseResult<CreateVehicleOutput>> Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            _appLogger.LogInformation("CreateVehicle: starting with ManufacturingDate {ManufacturingDate}", input.ManufacturingDate);
            try
            {
                var manufacturingDate = new ManufacturingDate(input.ManufacturingDate);
                var vehicleId = Guid.NewGuid();
                var vehicle = new Vehicle(vehicleId, manufacturingDate, isAvailable: true);

                await _vehicleRepository.Add(vehicle).ConfigureAwait(false);

                var output = new CreateVehicleOutput(
                    vehicle.Id,
                    manufacturingDate.Value,
                    vehicle.IsAvailable);
                _appLogger.LogInformation("CreateVehicle: completed. VehicleId {VehicleId}", vehicle.Id);
                return UseCaseResultBuilder.Success(output);
            }
            catch (VehicleTooOldForFleetException ex)
            {
                _appLogger.LogWarning("CreateVehicle: business validation failed. VehicleTooOldForFleet - {Message}", ex.Message);
                return UseCaseResultBuilder.Failure<CreateVehicleOutput>(UseCaseErrorCode.VehicleTooOldForFleet, ex.Message);
            }
        }
    }
}
