using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    [Collection(TestCollections.Functional)]
    public sealed class ListAvailableVehiclesFunctionalTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task ListAvailableReturnsOkWithEnvelope()
        {
            await Fixture
                .UsingHandlerForRequestResponse<ListAvailableVehiclesRequest, IActionResult>(async handler =>
                {
                    var result = await handler
                        .Handle(new ListAvailableVehiclesRequest(), CancellationToken.None);

                    result.Should().BeOfType<OkObjectResult>();
                    var ok = (OkObjectResult)result;
                    ok.Value.Should().NotBeNull();
                });
        }
    }
}
