namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Centralized error codes for use case failures. Used by <see cref="UseCaseResult{T}"/> and serialized as string in the API envelope.
    /// </summary>
    public enum UseCaseErrorCode
    {
        /// <summary>Vehicle manufacturing date is older than 5 years (fleet rule).</summary>
        VehicleTooOldForFleet,

        /// <summary>Vehicle was not found.</summary>
        VehicleNotFound,

        /// <summary>Vehicle is not available for rent.</summary>
        VehicleNotAvailable,

        /// <summary>Renter already has an active rental.</summary>
        RenterAlreadyHasActiveRental,

        /// <summary>Rental was not found or already returned.</summary>
        RentalNotFound,

        /// <summary>Resource not found (generic).</summary>
        NotFound,
    }
}
