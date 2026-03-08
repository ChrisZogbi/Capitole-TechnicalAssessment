using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MongoDb;
using Xunit;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public sealed class CompositionRootTestFixture : IDisposable, IAsyncLifetime
    {
        private const string MongoDbDatabaseName = "estimate-renting-functional-test";

        private MongoDbContainer _mongoContainer;
        private ServiceProvider _serviceProvider;
        private bool _initialized;

        public CompositionRootTestFixture()
        {
        }

        public IConfiguration Configuration { get; private set; }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            _mongoContainer = new MongoDbBuilder("mongo:7")
                .WithUsername(string.Empty)
                .WithPassword(string.Empty)
                .Build();

            await _mongoContainer.StartAsync().ConfigureAwait(false);

            var mongoConnectionString = _mongoContainer.GetConnectionString();

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["MongoDb:ConnectionString"] = mongoConnectionString,
                    ["MongoDb:MongoDbDatabaseName"] = MongoDbDatabaseName,
                })
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            _initialized = true;
        }

        public async Task UsingHandlerForRequest<TRequest>(Func<IRequestHandler<TRequest, Unit>, Task> handlerAction)
            where TRequest : IRequest
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, Unit>>();

            await handlerAction.Invoke(handler).ConfigureAwait(false);
        }

        public async Task UsingHandlerForRequestResponse<TRequest, TResponse>(Func<IRequestHandler<TRequest, TResponse>, Task> handlerAction)
            where TRequest : IRequest<TResponse>
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            if (handler == null)
            {
                Debug.Fail("The requested handler has not been registered");
            }

            await handlerAction.Invoke(handler).ConfigureAwait(false);
        }

        public async Task UsingRepository<TRepository>(Func<TRepository, Task> handlerAction)
        {
            ArgumentNullException.ThrowIfNull(handlerAction);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<TRepository>();

            if (handler == null)
            {
                Debug.Fail("The requested handler has not been registered");
            }

            await handlerAction.Invoke(handler).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();

            // Container is disposed in DisposeAsync (MongoDbContainer does not implement IDisposable).
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "S6966:Await DisposeAsync instead", Justification = "Disposing IAsyncDisposable in fixture lifecycle; await DisposeAsync() is correct.")]
        public async Task DisposeAsync()
        {
            _serviceProvider?.Dispose();

            if (_mongoContainer != null)
            {
                await _mongoContainer.DisposeAsync().ConfigureAwait(false);
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddApiDependencies();
            services.AddLogging();
            services.AddBaseInfrastructure(true);
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
            services.AddMongoRepositories();
        }
    }
}
