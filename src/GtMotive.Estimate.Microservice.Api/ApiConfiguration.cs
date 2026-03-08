using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GtMotive.Estimate.Microservice.Api.Authorization;
using GtMotive.Estimate.Microservice.Api.DependencyInjection;
using GtMotive.Estimate.Microservice.Api.Filters;
using GtMotive.Estimate.Microservice.ApplicationCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Api
{
    /// <summary>API configuration and registration extensions.</summary>
    [ExcludeFromCodeCoverage]
    public static class ApiConfiguration
    {
        /// <summary>Configures MVC options with API filters (e.g. business exception filter).</summary>
        /// <param name="options">The MVC options to configure.</param>
        public static void ConfigureControllers(MvcOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            options.Filters.Add<BusinessExceptionFilter>();
        }

        /// <summary>Adds API controllers from this assembly and registers API dependencies.</summary>
        /// <param name="builder">The MVC builder.</param>
        /// <returns>The same builder for chaining.</returns>
        public static IMvcBuilder WithApiControllers(this IMvcBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.AddApplicationPart(typeof(ApiConfiguration).GetTypeInfo().Assembly);

            AddApiDependencies(builder.Services);

            return builder;
        }

        /// <summary>Registers API dependencies (authorization, MediatR, use cases, presenters).</summary>
        /// <param name="services">The service collection.</param>
        public static void AddApiDependencies(this IServiceCollection services)
        {
            services.AddAuthorization(AuthorizationOptionsExtensions.Configure);
            services.AddMediatR(typeof(ApiConfiguration).GetTypeInfo().Assembly);
            services.AddUseCases();
            services.AddPresenters();
        }
    }
}
