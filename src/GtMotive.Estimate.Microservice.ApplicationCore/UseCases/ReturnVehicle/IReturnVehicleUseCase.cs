namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Use case for returning a rented vehicle.
    /// </summary>
    public interface IReturnVehicleUseCase : IUseCase<ReturnVehicleInput, UseCaseResult<ReturnVehicleOutput>>
    {
    }
}
