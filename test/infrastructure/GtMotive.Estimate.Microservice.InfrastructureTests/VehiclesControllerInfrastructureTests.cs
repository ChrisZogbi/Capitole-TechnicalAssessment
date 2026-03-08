using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Responses;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    /// <summary>
    /// Infrastructure tests for VehiclesController: host-level HTTP reception and response model validation.
    /// </summary>
    public sealed class VehiclesControllerInfrastructureTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        [Fact]
        public async Task ListAvailableReturns200AndEnvelopeWithData()
        {
            using var client = Fixture.Server.CreateClient();
            var response = await client.GetAsync(new Uri("/api/vehicles/available", UriKind.Relative));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var envelope = await response.Content
                .ReadFromJsonAsync<ApiResponse<ListAvailableVehiclesResponse>>(JsonOptions);

            Assert.NotNull(envelope);
            Assert.True(envelope.IsSuccess);
            Assert.NotNull(envelope.Data);
            Assert.NotNull(envelope.Data.Vehicles);
            Assert.Null(envelope.Error);
        }
    }
}
