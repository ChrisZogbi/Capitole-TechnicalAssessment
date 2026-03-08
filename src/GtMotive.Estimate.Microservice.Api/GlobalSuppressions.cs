// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "RequestResponseLoggingMiddleware uses simple log calls; LoggerMessage not required for this middleware.", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Api.Middleware.RequestResponseLoggingMiddleware")]
