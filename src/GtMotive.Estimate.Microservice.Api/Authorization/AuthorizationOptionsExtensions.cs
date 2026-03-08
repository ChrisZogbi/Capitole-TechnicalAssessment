using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace GtMotive.Estimate.Microservice.Api.Authorization
{
    /// <summary>Extensions for configuring authorization options.</summary>
    [ExcludeFromCodeCoverage]
    public static class AuthorizationOptionsExtensions
    {
        /// <summary>Configures authorization options.</summary>
        /// <param name="options">The authorization options to configure.</param>
        public static void Configure(AuthorizationOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);
        }
    }
}
