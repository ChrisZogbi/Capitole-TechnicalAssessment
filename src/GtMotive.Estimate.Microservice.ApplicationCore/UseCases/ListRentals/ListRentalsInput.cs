namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListRentals
{
    /// <summary>
    /// Input for listing rentals with optional filter by active state.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ListRentalsInput"/> class.
    /// </remarks>
    /// <param name="activeOnly">True = only active rentals, false = only returned, null = all.</param>
    public sealed class ListRentalsInput(bool? activeOnly)
    {
        /// <summary>
        /// Gets the optional filter: true = only active, false = only returned, null = all.
        /// </summary>
        public bool? ActiveOnly { get; } = activeOnly;
    }
}
