using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace GtMotive.Estimate.Microservice.UnitTests.Infrastructure
{
    /// <summary>
    /// Provides auto-generated test data with Moq for xUnit theories.
    /// </summary>
    internal sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(new AutoMoqCustomization());
                return fixture;
            })
        {
        }
    }
}
