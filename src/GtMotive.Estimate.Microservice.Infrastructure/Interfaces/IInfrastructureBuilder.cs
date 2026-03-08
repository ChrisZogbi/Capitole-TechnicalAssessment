using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Infrastructure.Interfaces
{
    /// <summary>Builder for infrastructure service registration.</summary>
    public interface IInfrastructureBuilder
    {
        /// <summary>Gets the service collection.</summary>
        IServiceCollection Services { get; }
    }
}
