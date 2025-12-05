# ERP Multi-tenant Ecosystem Bootstrap

Este workspace contiene la estructura inicial de repositorios separados para el ecosistema ERP multi-tenant. Cada carpeta corresponde a un repo independiente (specs, backend, frontends, infraestructura) listo para ser publicado en GitHub.

> Nota: En este entorno sin red no se pudieron crear/pushear los remotos en GitHub mediante `gh`, pero la estructura local está lista para conectarse y publicar cuando haya conectividad. Usa `gh repo create` y `git push origin main` en cada carpeta cuando haya red.

Repos incluidos:
- `erp-vibe-spec`: solo especificaciones, metadata y prompts en estilo vibe coding.
- `erp-backend`: skeleton ASP.NET Core con vertical slices y pipelines base.
- `erp-admin-frontend`: portal administrativo React + Tailwind con layout y página de tenants mock.
- `erp-tenant-frontend-template`: plantilla React + Tailwind para frontends por tenant con página de clientes mock.
- `erp-infra-admin-backend`: proyecto CDK (TypeScript) para backend + portal admin.
- `erp-infra-tenant-frontend`: proyecto CDK (TypeScript) para frontends de tenants.

## Publicación en GitHub

Pasos recomendados por cada carpeta cuando haya red y la CLI `gh` esté instalada:
1. `gh repo create <nombre-repo> --private --source=. --remote origin`
2. `git push -u origin main` (o la rama activa).

Nota de entorno: en este contenedor `gh` no está disponible y la instalación por `apt` falla por repositorios sin firma/403, por lo que el push debe realizarse cuando se disponga de conectividad y herramientas adecuadas.

### Bootstrap automatizado de remotos

Para acelerar la publicación cuando tengas red, usa `./scripts/bootstrap-remotes.sh`, que inicializa repos individuales, configura `origin` (vía `gh` si está disponible, o SSH manual) y opcionalmente hace commit/push.

1. Copia `.env.example` a `.env` y ajusta `GH_OWNER`, `VISIBILITY` y `AUTO_COMMIT` según prefieras.
2. Exporta las variables (por ejemplo `set -a; source .env; set +a`).
3. Ejecuta `./scripts/bootstrap-remotes.sh` desde la raíz del workspace.

Con `AUTO_COMMIT=false` (default) podrás revisar cada repo antes de publicar; con `AUTO_COMMIT=true` el script hará commit/push automático a `main` tras crear los remotos.
