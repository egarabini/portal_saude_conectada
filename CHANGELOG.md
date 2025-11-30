# Changelog - Portal Saúde Conectada

## [2025-11-25] - Documentação OpenAPI com Scalar

### ✨ [FEATURE] - Documentação Completa da API

**Implementado**: Documentação OpenAPI 3.0 completa com interface Scalar moderna.

**Recursos Adicionados**:
- ✅ **Scalar UI** - Interface moderna substituindo Swagger UI
- ✅ **13 endpoints documentados** com metadados completos
- ✅ **DTOs tipados** para todos os requests e responses
- ✅ **Exemplos de código** em múltiplas linguagens
- ✅ **Categorização** com emojis para melhor UX

**Endpoints Documentados**:
- 🌐 Público (3): status, catálogos, contato
- 📊 Admin - Métricas (2): overview, métricas por projeto
- 📚 Admin - Documentação (3): listar, obter, buscar
- 👥 Admin - Equipe (2): backlog, sprints
- 🔐 Autenticação (3): login, refresh, logout

**Como Acessar**:
- Execute o AppHost
- Acesse `https://localhost:7122/scalar/v1`
- Ou veja o JSON em `https://localhost:7122/openapi/v1.json`

**Arquivos Modificados**:
- `PortalSaudeConectada.Api/Program.cs` - Configuração OpenAPI e Scalar
- `Features/PublicEndpoints.cs` - Metadados e DTOs
- `Features/AdminMetricsEndpoints.cs` - Metadados e DTOs
- `Features/AdminDocsEndpoints.cs` - Metadados e DTOs
- `Features/AdminTeamEndpoints.cs` - Metadados e DTOs
- `Features/AuthEndpoints.cs` - Metadados e DTOs

**Pacotes Adicionados**:
- `Scalar.AspNetCore` v2.11.0

**Documentação**: Ver `Docs/README-OPENAPI.md` e `Docs/OPENAPI-SCALAR.md` para detalhes completos.

---

## [2024-11-24] - Configuração Inicial do Banco de Dados

### 🔧 [HOTFIX] - Correção da Connection String na API

**Problema**: Ao compilar a API standalone, ela não reconhecia a `DefaultConnection` porque a connection string só estava sendo passada via variável de ambiente pelo AppHost.

**Solução**: Adicionada a connection string nos arquivos `appsettings.json` e `appsettings.Development.json` da API.

**Arquivos Modificados**:
- `PortalSaudeConectada.Api/appsettings.json` - Adicionada `ConnectionStrings:DefaultConnection`
- `PortalSaudeConectada.Api/appsettings.Development.json` - Adicionada `ConnectionStrings:DefaultConnection` com logs detalhados

**Como Funciona Agora**:
- **Standalone**: API usa connection string do `appsettings.json`
- **Via AppHost**: AppHost injeta connection string via variável de ambiente (sobrescreve o appsettings)

**Documentação**: Ver `Docs/CONFIGURACAO-CONNECTION-STRING.md` para detalhes completos.

---

## [2024-11-24] - Configuração Inicial do Banco de Dados

### ✅ Alterações Realizadas

#### 1. Configuração do AppHost
- **Arquivo**: `PortalSaudeConectada.AppHost/AppHost.cs`
- **Mudança**: Configurado para passar a connection string via variável de ambiente para a API
- **Padrão**: Seguindo o mesmo padrão do ConectaRede
- **Variável**: `ConnectionStrings__DefaultConnection`

```csharp
const string defaultConnectionEnv = "ConnectionStrings__DefaultConnection";

var api = builder.AddProject<Projects.PortalSaudeConectada_Api>("api")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithEnvironment(defaultConnectionEnv, connectionString)
    .WithEnvironment(aspNetCoreEnvironment, applicationEnvironment);
```

#### 2. Schema do Banco de Dados
- **Arquivo**: `PortalSaudeConectada.Api/Data/PortalDbContext.cs`
- **Mudança**: Schema alterado de `portal` para `saude_conectada`
- **Objetivo**: Centralizar todo o controle do portal no schema `saude_conectada`

```csharp
modelBuilder.HasDefaultSchema("saude_conectada");
```

#### 3. Estrutura de Documentação
- **Pasta**: `PortalSaudeConectada/Docs/`
- **Arquivos Criados**:
  - `00-Indice.md` - Índice geral com instruções de gestão
  - `00-Resumo-Estruturado.md` - Visão consolidada do projeto
  - `99-Checklist-Aderencia.md` - Checklist de progresso
  - `README.md` - Guia completo de gestão da documentação

#### 4. Migration do Banco de Dados
- **Pasta**: `Desenvolvimento-Projeto/ConectaRede/ConectaRede.Database/Migrations/`
- **Arquivos Criados**:
  - `20251124000000_AddPortalSaudeConectadaSchema.cs` - Migration principal
  - `20251124000000_AddPortalSaudeConectadaSchema.Designer.cs` - Designer da migration
  - `README_PortalSaudeConectada.md` - Documentação da migration

#### 5. Scripts SQL
- **Pasta**: `Desenvolvimento-Projeto/ConectaRede/ConectaRede.Database/Scripts/`
- **Arquivos Criados**:
  - `CreatePortalSaudeConectadaSchema.sql` - Script de criação standalone
  - `VerifyPortalSaudeConectadaSchema.sql` - Script de verificação

### 🗄️ Estrutura do Banco de Dados

#### Schema: `saude_conectada`

Todas as tabelas do Portal Saúde Conectada estão neste schema:

##### Tabela: `audit_log`
Auditoria de acessos e ações no portal.

**Colunas:**
- `id` (SERIAL PRIMARY KEY)
- `timestamp` (TIMESTAMP NOT NULL, default: CURRENT_TIMESTAMP)
- `user_id` (VARCHAR(100))
- `action` (VARCHAR(200) NOT NULL)
- `resource` (VARCHAR(500))
- `details` (JSONB)
- `ip_address` (VARCHAR(45))

**Índices:**
- `idx_audit_log_timestamp`
- `idx_audit_log_user_id`

##### Tabela: `cached_metrics`
Cache de métricas agregadas dos projetos.

**Colunas:**
- `id` (SERIAL PRIMARY KEY)
- `project` (VARCHAR(100) NOT NULL)
- `metric_name` (VARCHAR(200) NOT NULL)
- `metric_value` (JSONB NOT NULL)
- `timestamp` (TIMESTAMP NOT NULL, default: CURRENT_TIMESTAMP)
- `expires_at` (TIMESTAMP)

**Índices:**
- `idx_cached_metrics_project_metric_timestamp`
- `idx_cached_metrics_expires_at`

##### Tabela: `doc_versions`
Versionamento completo da documentação.

**Colunas:**
- `id` (SERIAL PRIMARY KEY)
- `project` (VARCHAR(100) NOT NULL)
- `file_path` (VARCHAR(500) NOT NULL)
- `version` (VARCHAR(50) NOT NULL)
- `content` (TEXT NOT NULL)
- `author` (VARCHAR(100))
- `created_at` (TIMESTAMP NOT NULL, default: CURRENT_TIMESTAMP)
- `commit_hash` (VARCHAR(40))

**Constraints:**
- `uq_doc_versions_project_file_version` UNIQUE(project, file_path, version)

**Índices:**
- `idx_doc_versions_project_file_version`
- `idx_doc_versions_created_at`

### ✅ Testes Realizados

1. **Execução da Migration**: ✅ Sucesso
   ```
   Script executado com sucesso: CreatePortalSaudeConectadaSchema.sql
   ```

2. **Verificação do Schema**: ✅ Sucesso
   ```
   Script executado com sucesso: VerifyPortalSaudeConectadaSchema.sql
   ```

### 📝 Características Importantes

1. **IF NOT EXISTS**: Todos os comandos SQL usam `IF NOT EXISTS` para evitar erros
2. **Idempotência**: Scripts podem ser executados múltiplas vezes sem causar erros
3. **Comentários**: Todas as tabelas e colunas possuem comentários explicativos
4. **Índices Otimizados**: Índices criados para as consultas mais comuns
5. **JSONB**: Uso de JSONB para flexibilidade em `details` e `metric_value`

### 🔄 Próximos Passos

- [ ] Implementar endpoints de documentação em `AdminDocsEndpoints.cs`
- [ ] Criar serviço de sincronização Git → Banco de Dados
- [ ] Implementar busca full-text com PostgreSQL FTS
- [ ] Adicionar testes de integração para o PortalDbContext
- [ ] Implementar políticas de retenção de dados (90 dias para métricas)
- [ ] Configurar Keycloak realm `saude-conectada`

### 📚 Documentação Relacionada

- [Documentação do Portal](Docs/README.md)
- [Migration README](../Desenvolvimento-Projeto/ConectaRede/ConectaRede.Database/Migrations/README_PortalSaudeConectada.md)
- [Checklist de Aderência](Docs/99-Checklist-Aderencia.md)

