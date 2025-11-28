# ERP Infra Admin + Backend

Proyecto CDK (TypeScript) para infraestructura del backend y portal administrativo.

## Stacks previstos
- `vpc-stack.ts`: red base con subnets públicas/privadas.
- `db-stack.ts`: RDS Aurora Serverless PostgreSQL.
- `ecs-backend-stack.ts`: servicios ECS Fargate para APIs.
- `cognito-core-stack.ts`: user pool global de administración.
- `admin-frontend-stack.ts`: hosting de portal admin en Amplify/CloudFront.
- `networking-stack.ts`: ALB, WAF y certificados.

Pendiente: wiring entre stacks, parámetros y pipelines CI/CD.
