using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals
{
    /// <summary>
    /// Lists rentals with optional filter by active state (active only, returned only, or all).
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListRentalsUseCase"/> class.
    /// </remarks>
    /// <param name="rentalRepository">The rental repository.</param>
    /// <param name="appLogger">The application logger.</param>
    public sealed class ListRentalsUseCase(IRentalRepository rentalRepository, IAppLogger<ListRentalsUseCase> appLogger) : IListRentalsUseCase
    {
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IAppLogger<ListRentalsUseCase> _appLogger = appLogger;

        /// <inheritdoc/>
        public async Task<UseCaseResult<ListRentalsOutput>> Execute(ListRentalsInput input)
        {
            ArgumentNullException.ThrowIfNull(input);
            _appLogger.LogInformation("ListRentals: starting. ActiveOnly {ActiveOnly}", input.ActiveOnly);
            var rentals = await _rentalRepository.GetRentals(input.ActiveOnly).ConfigureAwait(false);
            var summaries = rentals
                .Select(r => new RentalSummary(r.Id, r.VehicleId, r.RenterId, r.StartDate, r.EndDate))
                .ToList();
            var output = new ListRentalsOutput(summaries);
            _appLogger.LogInformation("ListRentals: completed. Count {Count}", summaries.Count);
            return await Task.FromResult(UseCaseResultBuilder.Success(output)).ConfigureAwait(false);
        }
    }
}
