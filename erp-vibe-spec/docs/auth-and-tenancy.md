# Autenticación y multi-tenancy

- Cognito User Pool global para el portal administrativo.
- Cognito User Pool dedicado por tenant para el portal ERP.
- Multi-tenancy por columna `tenant_id` en todas las tablas de datos de negocio.
- Gobernanza de roles y claims por tipo de portal y tenant.
