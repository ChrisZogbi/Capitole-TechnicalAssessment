using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Extension methods for registering MongoDB persistence (client, connection, unit of work and repositories) in the DI container.
    /// </summary>
    public static class MongoDbServiceCollectionExtensions
    {
        /// <summary>
        /// Adds MongoDB persistence: registers <see cref="MongoService"/> (creates the connection when first resolved),
        /// <see cref="MongoDbUnitOfWork"/> (one per request, enables transactions for Rent/Return),
        /// and the vehicle and rental repositories.
        /// Call this after configuring MongoDb settings (e.g. with
        /// <c>Configure&lt;MongoDbSettings&gt;(configuration.GetSection("MongoDb"))</c>).
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            services.AddSingleton<MongoService>();
            services.AddScoped<MongoDbUnitOfWork>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MongoDbUnitOfWork>());
            services.AddScoped<IMongoSessionAccessor>(sp => sp.GetRequiredService<MongoDbUnitOfWork>());
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            return services;
        }
    }
}
