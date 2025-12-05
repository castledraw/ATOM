# ERP Backend

Skeleton de APIs ASP.NET Core con arquitectura Vertical Slice y Clean Architecture. Incluye servicios para Identity, Admin Portal y ERP Tenant, listos para integrar CQRS, Repository, Unit of Work y multi-tenancy por `tenant_id` usando GUID v7.

## Estructura
- `src/BuildingBlocks`: proyectos compartidos (`ERP.Shared.Domain`, `ERP.Shared.Application`, `ERP.Shared.Infrastructure`) que exponen entidades, contratos CQRS y repositorios reutilizables para cualquier API.
- `src/Services/IdentityService.Api`: servicio de identidad (placeholder).
- `src/Services/AdminPortal.Api`: servicio administrativo con slices para tenants, compañías, catálogos y despliegues.
- `src/Services/ErpTenant.Api`: servicio ERP por tenant con slices para clientes.
- `tests/UnitTests` y `tests/IntegrationTests`: suites de pruebas.

Los servicios usan un `InMemoryDataStore` compartido para demos y mocks; en siguientes iteraciones se conectará PostgreSQL/Cognito respetando los contratos definidos en BuildingBlocks.
