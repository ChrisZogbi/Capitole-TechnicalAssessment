## Capitole Technical Assessment – Renting Microservice

Este documento resume **qué se ha implementado** sobre la plantilla original y **cómo se ha adaptado el tema de autenticación** para la prueba técnica.

---

## Alcance funcional implementado

- **Dominio de Renting**
  - Entidades y reglas de negocio para **Vehicle** y **Rental** (incluyendo regla de “máximo 5 años de antigüedad” y “un alquiler activo por renter”).
  - Puertos de dominio `IVehicleRepository` e `IRentalRepository` para acceso a datos.

- **Casos de uso (ApplicationCore)**
  - `CreateVehicle`, `ListAvailableVehicles`, `RentVehicle`, `ReturnVehicle`.
  - Nuevo caso de uso **`ListRentals`** con filtro opcional `activeOnly` (activos / devueltos / todos).
  - Uso de `UseCaseResult<T>` + `UseCaseResultBuilder` para encapsular éxito/errores.

- **Infraestructura (MongoDB)**
  - Repositorios MongoDB para vehículos y alquileres.
  - Implementación de `GetRentals(bool? activeOnly)` filtrando por `EndDate` (null / no null / todos).
  - Transacciones MongoDB (replica set de un nodo) para operaciones Rent/Return.

- **API REST**
  - Endpoints principales:
    - `POST /api/vehicles`
    - `GET /api/vehicles/{id}`
    - `GET /api/vehicles/available`
    - `POST /api/rentals`
    - `POST /api/rentals/{id}/return`
    - `GET /api/rentals` con `?activeOnly=true|false` (o sin parámetro).
  - Todas las respuestas usan un **envelope homogéneo** `ApiResponse<T>` (`isSuccess`, `data`, `error`).
  - Integración con **MediatR** (requests/handlers por endpoint).

- **Ejecución local y pruebas manuales**
  - `docker-compose.yml` levanta **MongoDB en replica set** + **API** sin dependencias externas.
  - Documentado en `docs/pruebas-api-endpoints.md` un juego de pruebas manuales (happy paths y edge cases) ejecutadas contra la API real.

- **Pruebas automáticas**
  - Tests unitarios en `test/unit` para los casos de uso (incluyendo `ListRentalsUseCaseTests`).  
  - Tests funcionales / de infraestructura existentes de la plantilla siguen pasando.

---

## Autenticación y autorización

La plantilla original del microservicio está pensada para integrarse con **IdentityServer** (secciones de Authentication/Authorization, URL de IdentityServer, etc.).  
Para esta prueba técnica en concreto se ha decidido **no activar esa integración** por dos motivos:

- El **servidor de IdentityServer no está accesible** en el entorno de evaluación (no tengo credenciales ni endpoint operativo contra el que validar tokens).
- Para que la API funcionara “tal cual se levanta con `docker compose`”, habría que tener también configurada en IdentityServer la **URL de Swagger / callback** correspondiente; sin acceso al servidor de identidad, no es posible asegurar esa configuración ni probarla end‑to‑end.

Por ello:

- La API de renting se ha dejado **100 % preparada para añadir autenticación** (controladores, capas de aplicación y dominio son compatibles con `[Authorize]`, políticas, JWT bearer, etc.).
- Pero **no se ha activado auth en esta entrega**: todos los endpoints se exponen como **anónimos** para garantizar que el revisor puede levantar el stack local y probar la funcionalidad sin depender de un IdentityServer externo que no controla.

En una integración real, bastaría con:

- Configurar el esquema **JWT bearer** apuntando a la URL de IdentityServer proporcionada por el cliente.
- Añadir `[Authorize]`/políticas en los controladores según los roles necesarios.
- Alinear el `renterId` con el `sub` (u otro claim) del token emitido por IdentityServer, en lugar de recibirlo explícitamente en el body.

---

## Cómo ejecutar y probar

- **Levantar entorno** (sin instalar .NET ni MongoDB en local):
  - `docker compose up --build`
  - Swagger: `http://localhost:8080/swagger`

- **Smoke test mínimo**
  - `GET /api/vehicles/available` → `200 OK`, array (posiblemente vacío).
  - `POST /api/vehicles` → crea vehículo (201) si la fecha es ≤ 5 años.
  - `POST /api/rentals` → alquila un vehículo disponible (200).
  - `POST /api/rentals/{id}/return` → devuelve el alquiler (200).
  - `GET /api/rentals?activeOnly=true|false` → lista rentals activos/devueltos aplicando correctamente el filtro.

Para más detalle sobre los casos probados (incluyendo códigos de error y escenarios de conflicto) ver `docs/pruebas-api-endpoints.md`.
