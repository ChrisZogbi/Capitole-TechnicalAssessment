namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle's manufacturing date is older than 5 years.
    /// The fleet cannot contain vehicles with a manufacturing date superior to 5 years (in age).
    /// </summary>
    public sealed class VehicleTooOldForFleetException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        public VehicleTooOldForFleetException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleTooOldForFleetException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleTooOldForFleetException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
