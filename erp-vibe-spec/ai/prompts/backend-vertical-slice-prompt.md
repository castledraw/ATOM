# Backend Vertical Slice Prompt

Genera slices ASP.NET Core alineados a CQRS, Repository y Unit of Work.

- Expón **controllers MVC completos** (GET/POST/PUT/PATCH/DELETE) por módulo, siempre con Swagger/OpenAPI habilitado y comentarios XML.
- Usa building blocks compartidos (Domain + Application) para entidades, contratos CQRS y repositorios multi-tenant reutilizables.
- Implementa multi-tenant por `tenant_id` y `company_id`, aplicando filtros en queries y comandos con validaciones básicas.
- Asegura que cada API incluya endpoints de health, catálogos ERP (monedas, términos de pago, industrias, idiomas), tenants, companies, branches y customers listos para producción.
- Prepara los comandos para crear/actualizar recursos y deja puntos de extensión para Identity/Cognito y persistencia real (EF Core, DDD) en siguientes iteraciones.
