# Infra CDK Admin + Backend Prompt

Diseña stacks CDK en TypeScript para VPC, ALB/WAF, RDS Aurora PostgreSQL, ECS Fargate y CloudFront+S3 para el portal admin.

- Incluye security groups, listeners/target groups, logs y parámetros (CIDR, dominios, certificados ACM) listos para producción.
- Expone constructos reutilizables para cluster ECS, servicios backend (Admin, Tenant, Identity) y secretos de base de datos.
- Genera outputs/exports para enlazar pipelines CI/CD (build docker, deploy ECS, invalidar CloudFront).
- Prepara hooks para Route53 y Cognito (user pool admin) sin acoplar credenciales, usando variables de contexto.
