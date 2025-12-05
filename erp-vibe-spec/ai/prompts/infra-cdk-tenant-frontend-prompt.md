# Infra CDK Tenant Frontend Prompt

Genera stacks CDK para frontends por tenant (Amplify o S3+CloudFront) con dominios por subdominio.

- Define parámetros: tenantId, subdomain, userPoolId, apiUrl, certificados y zonas Route53.
- Crea buckets con versioning, distribución CloudFront con OAI, logs y behaviors cacheables.
- Expone outputs para pipelines que build/publish assets y actualizan registros DNS.
- Mantén los constructos desacoplados para clonar tenants masivamente sin duplicar código.
