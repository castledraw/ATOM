# Arquitectura ERP Multi-tenant (vibe coding)

## Frontend
- React + Tailwind.
- Portal Administrativo global para gestionar tenants, membresías, despliegues y facturación.
- Portal ERP por cliente (tenant) servido en subdominio propio y conectado a user pool dedicado.

## Backend
- ASP.NET Core con arquitectura Vertical Slice.
- APIs separadas por contexto (Identity, Admin Portal, ERP Tenant) sobre una base de datos PostgreSQL única.
- Patrones: CQRS (commands/queries), Repository, Unit of Work, Inversión de Control.
- Multi-tenant por columna `tenant_id` y claves primarias GUID v7.
- OpenAPI + Swagger UI por API.

## Autenticación
- AWS Cognito: un User Pool global para administración y un User Pool por tenant para el portal ERP.
- Integración con ASP.NET Identity en el backend.

## Infraestructura AWS
- VPC con subnets públicas y privadas.
- ECS Fargate para APIs detrás de Application Load Balancer + WAF.
- RDS Aurora Serverless (PostgreSQL) con réplicas de lectura.
- Route 53 para dominios y subdominios de tenants.
- Amplify + CloudFront para frontends (admin y plantillas de tenant).
- CDK para IaC y pipelines independientes por componente.
