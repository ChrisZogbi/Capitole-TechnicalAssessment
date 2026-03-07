namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a renter attempts to rent a vehicle but already has an active rental.
    /// A same person cannot have more than one vehicle rented at the same time.
    /// </summary>
    public sealed class RenterAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public RenterAlreadyHasActiveRentalException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RenterAlreadyHasActiveRentalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RenterAlreadyHasActiveRentalException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
