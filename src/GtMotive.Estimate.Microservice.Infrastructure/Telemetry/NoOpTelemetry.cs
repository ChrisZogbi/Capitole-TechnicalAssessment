using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Telemetry
{
    /// <summary>No-op telemetry implementation for development/testing.</summary>
    [ExcludeFromCodeCoverage]
    public class NoOpTelemetry : ITelemetry
    {
        /// <summary>Initializes a new instance of the <see cref="NoOpTelemetry"/> class.</summary>
        public NoOpTelemetry()
        {
        }

        /// <inheritdoc />
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            // Use for testing
        }

        /// <inheritdoc />
        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            // Use for testing
        }
    }
}
