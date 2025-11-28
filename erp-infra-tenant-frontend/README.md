# ERP Infra Tenant Frontend

Proyecto CDK para desplegar frontends ERP por tenant.

## Stack previsto
- `tenant-frontend-stack.ts`: crea Amplify App/S3+CloudFront, registros Route 53 y configura variables por tenant (tenantId, subdomain, userPoolId, apiUrl).

Pendiente: detalles de Amplify, certificados y orquestación de pipelines por tenant.
