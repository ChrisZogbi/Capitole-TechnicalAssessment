using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    internal sealed class Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
        public IWebHostEnvironment Environment { get; } = environment;

        public IConfiguration Configuration { get; } = configuration;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Required by UseStartup<T> convention.")]
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(TestServerDefaults.AuthenticationScheme)
                .AddTestServer();

            services.AddControllers(ApiConfiguration.ConfigureControllers)
                .WithApiControllers()
                .AddMvcOptions(options =>
                {
                    options.OutputFormatters.Insert(0, new StreamJsonOutputFormatter());
                });

            services.AddBaseInfrastructure(true);
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
            services.AddMongoRepositories();
        }
    }
}
