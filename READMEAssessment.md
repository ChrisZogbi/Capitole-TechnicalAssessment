## Capitole Technical Assessment – Renting Microservice

This document summarizes **what was implemented** on top of the original template and **how authentication was handled** for the assessment.

---

## Implemented scope

- **Renting domain**
  - Entities and business rules for **Vehicle** and **Rental** (including “maximum 5 years old” for vehicles and “one active rental per renter”).
  - Domain ports `IVehicleRepository` and `IRentalRepository` for data access.

- **Use cases (ApplicationCore)**
  - `CreateVehicle`, `ListAvailableVehicles`, `RentVehicle`, `ReturnVehicle`.
  - New **`ListRentals`** use case with optional `activeOnly` filter (active / returned / all).
  - Use of `UseCaseResult<T>` + `UseCaseResultBuilder` to encapsulate success / error.

- **Infrastructure (MongoDB)**
  - MongoDB repositories for vehicles and rentals.
  - Implementation of `GetRentals(bool? activeOnly)` filtering by `EndDate` (null / not null / all).
  - MongoDB transactions (single‑node replica set) for Rent/Return operations.

- **REST API**
  - Main endpoints:
    - `POST /api/vehicles`
    - `GET /api/vehicles/{id}`
    - `GET /api/vehicles/available`
    - `POST /api/rentals`
    - `POST /api/rentals/{id}/return`
    - `GET /api/rentals` con `?activeOnly=true|false` (o sin parámetro).
  - All responses use a **homogeneous envelope** `ApiResponse<T>` (`isSuccess`, `data`, `error`).
  - Integration with **MediatR** (request/handler per endpoint).

- **Local run and manual testing**
  - `docker-compose.yml` starts **MongoDB replica set** + **API** with no external dependencies.
  - `docs/pruebas-api-endpoints.md` documents a manual test suite (happy paths and edge cases) executed against the running API.

- **Automated tests**
  - Unit tests in `test/unit` for the use cases (including `ListRentalsUseCaseTests`).  
  - Existing functional / infrastructure tests from the template still pass.

---

## Authentication and authorization

The original template is designed to integrate with **IdentityServer** (Authentication/Authorization sections, IdentityServer URL, etc.).  
For this assessment I decided **not to enable that integration**, for two reasons:

- The **IdentityServer instance is not accessible** in the evaluation environment (no credentials or live endpoint to validate tokens against).
- To make sure the API “just works with `docker compose up`”, IdentityServer would also need to be configured with the correct **Swagger URL / callback**; without access to that server, I cannot guarantee or test that end‑to‑end.

Because of that:

- The renting API is left **100% ready for authentication to be added** (controllers, application layer and domain are compatible with `[Authorize]`, policies, JWT bearer, etc.).
- But **auth is not enabled in this delivery**: all endpoints are **anonymous**, so the reviewer can run the stack locally and test the functionality without depending on an external IdentityServer they do not control.

In a real integration it would be enough to:

- Configure ASP.NET Core **JWT bearer authentication** pointing to the client’s IdentityServer URL.
- Add `[Authorize]` and policies on controllers according to required roles.
- Align `renterId` with the `sub` (or another claim) of the token issued by IdentityServer, instead of receiving it explicitly in the request body.

---

## How to run and smoke test

- **Run locally** (no need to install .NET or MongoDB):
  - `docker compose up --build`
  - Swagger: `http://localhost:8080/swagger`

- **Minimal smoke test**
  - `GET /api/vehicles/available` → `200 OK`, array (possibly empty).
  - `POST /api/vehicles` → creates a vehicle (`201 Created`) if manufacturing date is ≤ 5 years.
  - `POST /api/rentals` → rents an available vehicle (`200 OK`).
  - `POST /api/rentals/{id}/return` → returns the rental (`200 OK`).
  - `GET /api/rentals?activeOnly=true|false` → lists active/returned rentals applying the filter correctly.
