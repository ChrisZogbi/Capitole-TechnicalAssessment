namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Use case for creating a new vehicle in the fleet.
    /// </summary>
    public interface ICreateVehicleUseCase : IUseCase<CreateVehicleInput, UseCaseResult<CreateVehicleOutput>>
    {
    }
}
