using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Rents a vehicle to a renter. Enforces: vehicle must exist and be available; renter cannot have more than one active rental.
    /// Uses <see cref="IUnitOfWork"/> so that Add(rental) and Update(vehicle) are committed atomically.
    /// </summary>
    public sealed class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        IAppLogger<RentVehicleUseCase> appLogger) : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAppLogger<RentVehicleUseCase> _appLogger = appLogger;

        /// <inheritdoc/>
        public async Task<UseCaseResult<RentVehicleOutput>> Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            _appLogger.LogInformation("RentVehicle: starting. VehicleId {VehicleId}, RenterId {RenterId}", input.VehicleId, input.RenterId);
            var vehicle = await _vehicleRepository.GetById(input.VehicleId).ConfigureAwait(false);
            if (vehicle == null)
            {
                _appLogger.LogWarning("RentVehicle: vehicle not found. VehicleId {VehicleId}", input.VehicleId);
                return UseCaseResultBuilder.Failure<RentVehicleOutput>(UseCaseErrorCode.NotFound, "The vehicle was not found.");
            }

            if (!vehicle.IsAvailable)
            {
                _appLogger.LogWarning("RentVehicle: vehicle not available. VehicleId {VehicleId}", input.VehicleId);
                return UseCaseResultBuilder.Failure<RentVehicleOutput>(UseCaseErrorCode.VehicleNotAvailable, "The vehicle is not available for rent.");
            }

            var activeRental = await _rentalRepository.GetActiveByRenter(input.RenterId).ConfigureAwait(false);
            if (activeRental != null)
            {
                _appLogger.LogWarning("RentVehicle: renter already has active rental. RenterId {RenterId}", input.RenterId);
                return UseCaseResultBuilder.Failure<RentVehicleOutput>(UseCaseErrorCode.RenterAlreadyHasActiveRental, "The renter already has an active rental. Only one vehicle per renter at a time.");
            }

            var rentalId = Guid.NewGuid();
            var startDate = DateTime.UtcNow;
            var rental = new Rental(rentalId, vehicle.Id, input.RenterId, startDate, endDate: null);

            vehicle.MarkAsRented();

            await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await _rentalRepository.Add(rental).ConfigureAwait(false);
            await _vehicleRepository.Update(vehicle).ConfigureAwait(false);
            await _unitOfWork.Save().ConfigureAwait(false);

            _appLogger.LogInformation("RentVehicle: completed. RentalId {RentalId}, VehicleId {VehicleId}", rental.Id, vehicle.Id);
            var output = new RentVehicleOutput(rental.Id, rental.VehicleId, rental.RenterId, rental.StartDate);
            return UseCaseResultBuilder.Success(output);
        }
    }
}
