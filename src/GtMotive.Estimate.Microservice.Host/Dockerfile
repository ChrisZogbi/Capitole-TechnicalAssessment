# See https://aka.ms/containerfastmode for how Visual Studio uses this Dockerfile for faster debugging.
# Uses official .NET images and NuGet.org only (no private feeds or tokens required).

FROM mcr.microsoft.com/dotnet/aspnet:9.0.0-noble-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0.100-noble-amd64 AS build
WORKDIR /src
COPY Directory.Build.* ./
COPY NuGet.config .
COPY ["src/GtMotive.Estimate.Microservice.Domain/GtMotive.Estimate.Microservice.Domain.csproj", "GtMotive.Estimate.Microservice.Domain/"]
COPY ["src/GtMotive.Estimate.Microservice.ApplicationCore/GtMotive.Estimate.Microservice.ApplicationCore.csproj", "GtMotive.Estimate.Microservice.ApplicationCore/"]
COPY ["src/GtMotive.Estimate.Microservice.Infrastructure/GtMotive.Estimate.Microservice.Infrastructure.csproj", "GtMotive.Estimate.Microservice.Infrastructure/"]
COPY ["src/GtMotive.Estimate.Microservice.Api/GtMotive.Estimate.Microservice.Api.csproj", "GtMotive.Estimate.Microservice.Api/"]
COPY ["src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj", "GtMotive.Estimate.Microservice.Host/"]
RUN dotnet restore "GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj"
COPY src .
COPY . .
WORKDIR "/src/GtMotive.Estimate.Microservice.Host"
RUN dotnet build "GtMotive.Estimate.Microservice.Host.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/GtMotive.Estimate.Microservice.Host"
RUN dotnet publish "GtMotive.Estimate.Microservice.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]