using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.ApplicationInsights;

namespace GtMotive.Estimate.Microservice.Infrastructure.Telemetry
{
    /// <summary>Application Insights implementation of <see cref="ITelemetry"/>.</summary>
    [ExcludeFromCodeCoverage]
    public class AppTelemetry : ITelemetry
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>Initializes a new instance of the <see cref="AppTelemetry"/> class.</summary>
        /// <param name="telemetry">The Application Insights telemetry client.</param>
        public AppTelemetry(TelemetryClient telemetry)
        {
            _telemetryClient = telemetry;
        }

        /// <inheritdoc />
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        /// <inheritdoc />
        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackMetric(name, value, properties);
        }
    }
}
