namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle.
    /// </summary>
    public interface IRentVehicleUseCase : IUseCase<RentVehicleInput, UseCaseResult<RentVehicleOutput>>
    {
    }
}
