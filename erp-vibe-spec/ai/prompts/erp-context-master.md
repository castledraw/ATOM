# ERP Context Master (Arquitecto IA)

Sistema ERP multi-tenant, modular y responsive.

Dominios clave:
- Gestión de clientes con teléfonos múltiples, direcciones jerárquicas (país, estado, ciudad, municipio, distrito, barrio), redes sociales, emails y notas.
- Gestión de suscripciones de empresas (tenants del ERP).
- Gestión de pagos de suscriptores.
- Tipos de membresía con módulos habilitados.
- Gestión de despliegues del portal ERP para cada cliente (tenant).

Arquitectura técnica:
- Frontend: ReactJS + Tailwind. Portal Administrativo global y portal ERP por cliente en subdominio propio.
- Backend: ASP.NET Core con arquitectura Vertical Slice. Múltiples APIs tipo microservicio pero con base de datos única PostgreSQL (RDS Aurora Serverless). PK GUID v7. Patrones Repository, CQRS (commands/queries separados), Unit of Work, IoC. OpenAPI + Swagger UI por API. Multi-tenant por columna `tenant_id`.
- Autenticación: AWS Cognito + ASP.NET Identity. Un User Pool global para administración y un User Pool por tenant para el portal ERP de cada cliente.
- Infra AWS (CDK): VPC con subnets públicas y privadas; ECS Fargate para APIs; RDS Aurora Serverless PostgreSQL con réplicas de lectura; Cognito; Application Load Balancer; WAF; Route 53; Amplify + CloudFront para frontends (admin + tenants).
- CI/CD: Pipelines independientes para backend, portal admin y plantilla de portal tenant.

Instrucciones vibe coding: sintetiza, mantén decisiones claras y deja espacio para exploración creativa de IA en iteraciones futuras.
