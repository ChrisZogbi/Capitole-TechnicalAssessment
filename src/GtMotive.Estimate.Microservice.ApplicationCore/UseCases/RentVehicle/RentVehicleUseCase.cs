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
        IUnitOfWork unitOfWork) : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <inheritdoc/>
        public async Task<UseCaseResult<RentVehicleOutput>> Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            var vehicle = await _vehicleRepository.GetById(input.VehicleId).ConfigureAwait(false);
            if (vehicle == null)
            {
                return UseCaseResultBuilder.Failure<RentVehicleOutput>("NotFound", "The vehicle was not found.");
            }

            if (!vehicle.IsAvailable)
            {
                return UseCaseResultBuilder.Failure<RentVehicleOutput>("VehicleNotAvailable", "The vehicle is not available for rent.");
            }

            var activeRental = await _rentalRepository.GetActiveByRenter(input.RenterId).ConfigureAwait(false);
            if (activeRental != null)
            {
                return UseCaseResultBuilder.Failure<RentVehicleOutput>("RenterAlreadyHasActiveRental", "The renter already has an active rental. Only one vehicle per renter at a time.");
            }

            var rentalId = Guid.NewGuid();
            var startDate = DateTime.UtcNow;
            var rental = new Rental(rentalId, vehicle.Id, input.RenterId, startDate, endDate: null);

            vehicle.MarkAsRented();

            await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await _rentalRepository.Add(rental).ConfigureAwait(false);
            await _vehicleRepository.Update(vehicle).ConfigureAwait(false);
            await _unitOfWork.Save().ConfigureAwait(false);

            var output = new RentVehicleOutput(rental.Id, rental.VehicleId, rental.RenterId, rental.StartDate);
            return UseCaseResultBuilder.Success(output);
        }
    }
}
