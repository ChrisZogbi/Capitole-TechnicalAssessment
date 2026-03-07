using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Returns a rented vehicle. Marks the rental as ended and the vehicle as available.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
    /// </remarks>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="rentalRepository">The rental repository.</param>
    public sealed class ReturnVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository) : IReturnVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;

        /// <inheritdoc/>
        public async Task<ReturnVehicleResult> Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            var rental = await _rentalRepository.GetById(input.RentalId).ConfigureAwait(false);
            if (rental == null)
            {
                return ReturnVehicleResult.Failure("NotFound", "The rental was not found.");
            }

            if (!rental.IsActive)
            {
                return ReturnVehicleResult.Failure("NotFound", "The rental has already been returned.");
            }

            var vehicle = await _vehicleRepository.GetById(rental.VehicleId).ConfigureAwait(false);
            if (vehicle == null)
            {
                return ReturnVehicleResult.Failure("NotFound", "The vehicle was not found.");
            }

            var endDate = DateTime.UtcNow;
            rental.MarkAsReturned(endDate);
            vehicle.MarkAsReturned();

            await _rentalRepository.Update(rental).ConfigureAwait(false);
            await _vehicleRepository.Update(vehicle).ConfigureAwait(false);

            var output = new ReturnVehicleOutput(rental.Id, rental.VehicleId, endDate);
            return ReturnVehicleResult.Success(output);
        }
    }
}
