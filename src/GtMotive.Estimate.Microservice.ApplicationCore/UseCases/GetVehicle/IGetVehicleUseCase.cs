using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetVehicle
{
    /// <summary>
    /// Use case for getting a vehicle by identifier.
    /// </summary>
    public interface IGetVehicleUseCase : IUseCase<GetVehicleInput, UseCaseResult<VehicleSummary>>
    {
    }
}
