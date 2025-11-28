# Infraestructura AWS

- VPC con subnets públicas/privadas y seguridad gestionada por WAF.
- ECS Fargate para APIs con ALB y auto scaling.
- RDS Aurora Serverless PostgreSQL con réplicas de lectura.
- Cognito para autenticación (global + por tenant).
- Route 53 para dominios y subdominios por tenant.
- Amplify + CloudFront para frontends admin y plantillas de tenant.
