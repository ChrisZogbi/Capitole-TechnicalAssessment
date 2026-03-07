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
    public sealed class CreateVehicleUseCase(IVehicleRepository vehicleRepository) : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        /// <inheritdoc/>
        public async Task<CreateVehicleResult> Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
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
                return CreateVehicleResult.Success(output);
            }
            catch (VehicleTooOldForFleetException ex)
            {
                return CreateVehicleResult.Failure("VehicleTooOldForFleet", ex.Message);
            }
        }
    }
}
