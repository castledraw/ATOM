# ERP Backend

Skeleton inicial para APIs ASP.NET Core con arquitectura Vertical Slice. Incluye servicios para Identity, Admin Portal y ERP Tenant, listos para integrar CQRS, Repository, Unit of Work y multi-tenancy por `tenant_id` usando GUID v7.

## Estructura
- `src/BuildingBlocks`: componentes compartidos y cross-cutting (pendiente).
- `src/Services/IdentityService.Api`: servicio de identidad (placeholder).
- `src/Services/AdminPortal.Api`: servicio administrativo con slices para tenants, suscripciones y despliegues.
- `src/Services/ErpTenant.Api`: servicio ERP por tenant (placeholder).
- `tests/UnitTests` y `tests/IntegrationTests`: suites de pruebas.

Comentarios y TODOs indican dónde conectar Cognito, PostgreSQL y pipelines CQRS en iteraciones futuras.
