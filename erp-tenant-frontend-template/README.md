# ERP Tenant Frontend Template

Plantilla React + Tailwind para portales ERP por tenant. Sirve como base para generar frontends dedicados vía CDK/Amplify.

## Notas
- Página `ClientsListPage` con tabla mock en `src/modules/clients/pages/ClientsListPage.tsx`.
- Se conectará a un user pool de Cognito específico por tenant y APIs multi-tenant.
- Este repositorio será duplicado por tenant con configuración de subdominios y pipelines propios.
