using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Value object representing the manufacturing date of a vehicle.
    /// The fleet cannot contain vehicles whose manufacturing date is more than 5 years in the past (superior a 5 años).
    /// </summary>
    public readonly struct ManufacturingDate
    {
        private const int MaximumAgeInYears = 5;
        private readonly DateTime _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturingDate"/> struct.
        /// </summary>
        /// <param name="value">The manufacturing date.</param>
        /// <exception cref="VehicleTooOldForFleetException">Thrown when the manufacturing date is older than 5 years.</exception>
        public ManufacturingDate(DateTime value)
        {
            var utcDate = value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
            var dateOnly = utcDate.Date;

            var today = DateTime.UtcNow.Date;
            var minimumDate = today.AddYears(-MaximumAgeInYears);
            if (dateOnly < minimumDate)
            {
                throw new VehicleTooOldForFleetException(
                    $"The fleet cannot contain vehicles with a manufacturing date older than {MaximumAgeInYears} years.");
            }

            _value = dateOnly;
        }

        /// <summary>
        /// Gets the date value.
        /// </summary>
        public DateTime Value => _value;

        /// <inheritdoc/>
        public override string ToString() => _value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    }
}
