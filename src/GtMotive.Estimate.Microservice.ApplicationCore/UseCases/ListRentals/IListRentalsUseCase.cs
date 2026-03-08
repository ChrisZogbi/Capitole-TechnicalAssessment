namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals
{
    /// <summary>
    /// Use case for listing rentals with optional filter (active only / returned only / all).
    /// </summary>
    public interface IListRentalsUseCase : IUseCase<ListRentalsInput, UseCaseResult<ListRentalsOutput>>
    {
    }
}
