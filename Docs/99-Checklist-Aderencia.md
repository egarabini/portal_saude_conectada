# Checklist de Aderência — Portal Saúde Conectada

## Arquitetura & Stack
- [x] .NET Aspire (AppHost/ServiceDefaults) para orquestração em dev
- [x] .NET 10 como target framework
- [ ] Blazor WASM + MudBlazor (componentes de dashboard, charts, grids)
- [ ] Minimal APIs por Feature (Vertical Slice): Public, Admin/Metrics, Admin/Docs, Admin/Team, Auth
- [ ] API First (contratos versionados para APIs públicas e autenticadas)
- [x] Model First (schema Postgres `saude_conectada` definido)

## Frontend — Face Pública
- [ ] Landing page implementada (hero + features + CTA)
- [ ] Conteúdo institucional (missão, produtos, FAQ)
- [ ] Formulário de contato/adesão funcional
- [ ] SEO otimizado (meta tags, structured data, sitemap)
- [ ] Acessibilidade WCAG AA (navegação teclado, ARIA, contraste)
- [ ] Performance (Lighthouse score ≥ 90)

## Frontend — Face Autenticada (Dashboard Admin)
- [ ] Login via Keycloak com role `admin-portal`
- [ ] Visão Geral: cards de status por projeto (CarePlanner/ConectaRede/MCP)
- [ ] Checklists consolidados (integração com CSV, progresso visual)
- [ ] Documentação navegável (Escopo/Funcional/Técnico/Blueprint por projeto)
- [ ] Busca full-text nos docs
- [ ] Métricas técnicas: integração OTel (uptime, latência p95, erros)
- [ ] Gestão de Equipe: backlog, sprints, alocação (integração DevOps API)
- [ ] Interação: comentários em docs, export de relatórios (PDF/Excel)

## Backend — APIs
- [ ] `Features/Public`: `/status`, `/catalogos`, `/contato` (POST)
- [ ] `Features/Admin/Metrics`: `/api/admin/metrics/overview`, `/api/admin/metrics/{projeto}`
- [ ] `Features/Admin/Docs`: `/api/admin/docs`, `/api/admin/docs/{projeto}/{arquivo}`
- [ ] `Features/Admin/Team`: `/api/admin/backlog`, `/api/admin/sprints`
- [ ] `Features/Auth`: `/api/auth/login`, `/api/auth/refresh`, `/api/auth/logout`
- [ ] Validação JWT em todas as rotas `/api/admin/*`
- [ ] Rate limiting por IP em rotas públicas

## Dados
- [x] Postgres schema `saude_conectada` criado
- [x] Tabelas: `audit_log`, `cached_metrics`, `doc_versions`
- [ ] Migrações versionadas (EF Core ou Dapper)
- [ ] Políticas de retenção (90 dias para métricas não-críticas)
- [ ] Integração read-only com GCCuidado/HAPI FHIR via APIs

## Segurança & LGPD
- [ ] Keycloak realm `saude-conectada` configurado
- [ ] Client `portal-admin` com role `admin-portal`
- [ ] JWT validado com refresh tokens
- [ ] TLS obrigatório em produção
- [ ] Segredos gerenciados via Azure Key Vault ou equivalente
- [ ] CORS configurado (público: amplo; admin: restrito)
- [ ] Mascaramento de PII em logs
- [ ] Banner de consentimento para cookies analytics (face pública)
- [ ] Auditoria de acessos ao dashboard admin

## Observabilidade & Resiliência
- [ ] HealthChecks `/healthz` valida Postgres, Keycloak, APIs externas
- [ ] OpenTelemetry: traces/metrics end-to-end com correlation-id
- [ ] Dashboards Grafana/Azure Monitor (uptime, latência, 4xx/5xx)
- [ ] Alertas: downtime, latência p95 > threshold, erros auth
- [ ] Circuit breaker para integrações externas (DevOps APIs, OTel)
- [ ] Retries com backoff exponencial

## Integrações
- [ ] CarePlanner/ConectaRede/IntelliCare MCP: consumo de métricas via APIs
- [ ] Azure DevOps/Jira (opcional): backlog/sprints
- [ ] GitHub (opcional): PRs, commits, pipeline status
- [ ] OTel Collector: export para Azure Monitor ou Grafana

## Entrega & DevOps
- [ ] Build estático (WASM) servido por CDN (Azure Static Web Apps)
- [ ] APIs containerizadas (Docker) com deploy via CI/CD
- [ ] Feature flags implementados (`dashboard-enabled`, `team-backlog-enabled`)
- [ ] Pipelines de build/test/release (GitHub Actions ou Azure Pipelines)
- [ ] Ambientes: dev, staging, prod
- [ ] Rollback automatizado em caso de falha
- [ ] Documentação de deploy atualizada

## Roadmap & Priorização
- [ ] MVP Público concluído (landing + institucional + SEO)
- [ ] MVP Autenticado concluído (login + overview + checklists)
- [ ] Documentação navegável implementada (busca + versionamento)
- [ ] Métricas técnicas integradas (OTel + charts)
- [ ] Gestão de Equipe implementada (backlog + sprints + notificações)

## Configuração do Banco de Dados

### Schema: saude_conectada
Todas as tabelas do Portal Saúde Conectada estão no schema `saude_conectada`:

- **audit_log**: Auditoria de acessos e ações no portal
- **cached_metrics**: Cache de métricas agregadas dos projetos
- **doc_versions**: Versionamento completo da documentação

### Connection String
A connection string é gerenciada pelo AppHost e passada via variável de ambiente:
- Variável: `ConnectionStrings__DefaultConnection`
- Configurada em: `PortalSaudeConectada.AppHost/appsettings.json`
- Target configurável via: `Database:Target`

