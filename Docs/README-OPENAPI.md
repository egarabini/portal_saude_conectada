# 🎯 Guia Rápido - Documentação OpenAPI

## ✅ O Que Foi Implementado

### 1. OpenAPI 3.0 Configurado
- ✅ Metadados completos da API
- ✅ Informações de contato e licença
- ✅ Descrição detalhada de funcionalidades
- ✅ Servidores de desenvolvimento configurados

### 2. Scalar UI Integrado
- ✅ Interface moderna (substituindo Swagger UI)
- ✅ Tema roxo personalizado
- ✅ Exemplos de código em C#
- ✅ Suporte a autenticação Bearer

### 3. Todos os Endpoints Documentados

#### 🌐 Público (3 endpoints)
- `GET /api/public/status` - Status do sistema
- `GET /api/public/catalogos` - Catálogo de projetos e features
- `POST /api/public/contato` - Enviar mensagem de contato

#### 📊 Admin - Métricas (2 endpoints)
- `GET /api/admin/metrics/overview` - Visão geral das métricas
- `GET /api/admin/metrics/{projeto}` - Métricas de um projeto específico

#### 📚 Admin - Documentação (3 endpoints)
- `GET /api/admin/docs` - Listar documentos (com filtro opcional por projeto)
- `GET /api/admin/docs/{projeto}/{filepath}` - Obter documento com histórico de versões
- `POST /api/admin/docs/search` - Buscar documentos (full-text search)

#### 👥 Admin - Equipe (2 endpoints)
- `GET /api/admin/team/backlog` - Obter backlog de todos os projetos
- `GET /api/admin/team/sprints` - Obter informações sobre sprints

#### 🔐 Autenticação (3 endpoints)
- `POST /api/auth/login` - Fazer login e obter tokens
- `POST /api/auth/refresh` - Renovar token de acesso
- `POST /api/auth/logout` - Invalidar tokens

**Total: 13 endpoints documentados**

### 4. DTOs Tipados

Todos os endpoints usam DTOs (Data Transfer Objects) tipados para garantir documentação precisa:

- ✅ `StatusResponse`, `CatalogosResponse`, `ContatoResponse`
- ✅ `MetricsOverviewResponse`, `ProjectMetricsResponse`
- ✅ `DocsListResponse`, `DocDetailResponse`, `SearchResponse`
- ✅ `BacklogResponse`, `SprintsResponse`
- ✅ `LoginResponse`, `RefreshResponse`, `LogoutResponse`

### 5. Metadados Detalhados

Cada endpoint inclui:
- ✅ **Nome** (WithName)
- ✅ **Resumo** (WithSummary)
- ✅ **Descrição** (WithDescription)
- ✅ **Tipos de Request** (Accepts)
- ✅ **Tipos de Response** (Produces)
- ✅ **Códigos de Status** (200, 400, 401, 404, etc.)

## 🚀 Como Acessar

### Passo 1: Execute o AppHost

```bash
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
```

### Passo 2: Acesse a Documentação

Abra seu navegador em uma das URLs:

- **Scalar UI (HTTPS)**: https://localhost:7122/scalar/v1
- **Scalar UI (HTTP)**: http://localhost:5087/scalar/v1
- **OpenAPI JSON**: https://localhost:7122/openapi/v1.json

### Passo 3: Explore a Documentação

1. **Navegue pelas categorias** (tags) no menu lateral
2. **Clique em um endpoint** para ver detalhes
3. **Teste o endpoint** clicando em "Send Request"
4. **Veja exemplos de código** em diferentes linguagens

## 🎨 Recursos do Scalar

### Interface Moderna
- Design limpo e responsivo
- Tema roxo personalizado
- Busca rápida de endpoints
- Navegação intuitiva por categorias

### Testando Endpoints
1. Selecione um endpoint
2. Preencha os parâmetros (se necessário)
3. Clique em "Send Request"
4. Veja a resposta em tempo real

### Exemplos de Código
Scalar gera automaticamente exemplos em:
- C# (HttpClient)
- JavaScript (fetch)
- Python (requests)
- cURL

### Autenticação
Para testar endpoints protegidos:
1. Faça login via `POST /api/auth/login`
2. Copie o `accessToken`
3. Clique em "Authorize"
4. Cole o token

## 📊 Comparação: Scalar vs Swagger UI

| Recurso | Scalar | Swagger UI |
|---------|--------|------------|
| Design Moderno | ✅ | ❌ |
| Performance | ✅ Rápido | ⚠️ Lento |
| Temas | ✅ Múltiplos | ⚠️ Limitado |
| Exemplos de Código | ✅ Múltiplas linguagens | ⚠️ Apenas cURL |
| Busca | ✅ Rápida | ⚠️ Básica |
| Responsivo | ✅ | ⚠️ |

## 📝 Exemplo de Uso

### 1. Testar Status do Sistema

```bash
curl -X GET "https://localhost:7122/api/public/status"
```

Resposta:
```json
{
  "status": "healthy",
  "version": "1.0.0",
  "timestamp": "2025-11-25T10:30:00Z",
  "services": {
    "carePlanner": "operational",
    "conectaRede": "operational",
    "intelliCareMcp": "operational"
  }
}
```

### 2. Fazer Login

```bash
curl -X POST "https://localhost:7122/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@portal.local",
    "password": "demo123"
  }'
```

Resposta:
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "expiresIn": 3600,
  "user": {
    "id": "admin-1",
    "email": "admin@portal.local",
    "name": "Administrador Portal",
    "roles": ["admin-portal"]
  }
}
```

### 3. Obter Métricas (com autenticação)

```bash
curl -X GET "https://localhost:7122/api/admin/metrics/overview" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

## 🔧 Arquivos Modificados

1. ✅ `Program.cs` - Configuração OpenAPI e Scalar
2. ✅ `PublicEndpoints.cs` - Metadados e DTOs
3. ✅ `AdminMetricsEndpoints.cs` - Metadados e DTOs
4. ✅ `AdminDocsEndpoints.cs` - Metadados e DTOs
5. ✅ `AdminTeamEndpoints.cs` - Metadados e DTOs
6. ✅ `AuthEndpoints.cs` - Metadados e DTOs

## 📚 Documentação Adicional

- [OPENAPI-SCALAR.md](./OPENAPI-SCALAR.md) - Guia completo do Scalar
- [EXECUCAO-VIA-APPHOST.md](./EXECUCAO-VIA-APPHOST.md) - Como executar via AppHost
- [CONFIGURACAO-CONNECTION-STRING.md](./CONFIGURACAO-CONNECTION-STRING.md) - Configuração de banco

## 🎯 Próximos Passos

1. ✅ OpenAPI configurado
2. ✅ Scalar integrado
3. ✅ Endpoints documentados
4. 🔲 Adicionar validação de request (FluentValidation)
5. 🔲 Adicionar autenticação JWT real (Keycloak)
6. 🔲 Adicionar rate limiting
7. 🔲 Adicionar versionamento de API (v2, v3, etc.)

---

**Pronto para usar!** 🎉

Execute o AppHost e acesse `https://localhost:7122/scalar/v1` para ver a documentação completa da API.

