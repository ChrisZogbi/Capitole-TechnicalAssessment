namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Use case for listing vehicles available for rent.
    /// </summary>
    public interface IListAvailableVehiclesUseCase : IUseCase<ListAvailableVehiclesInput, UseCaseResult<ListAvailableVehiclesOutput>>
    {
    }
}
