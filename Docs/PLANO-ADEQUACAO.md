# 🎯 Plano de Adequação - PortalSaudeConectada

> **Baseado em**: BIBLIOTECA_MODELO  
> **Data**: 2025-11-29  
> **Status**: Planejamento

---

## 📋 Fases do Projeto

### FASE 1: Reestruturação da Solução (1-2 dias)

#### 1.1 Criar Projeto Shared
```bash
dotnet new classlib -n PortalSaudeConectada.Shared -o PortalSaudeConectada/PortalSaudeConectada.Shared
dotnet sln add PortalSaudeConectada/PortalSaudeConectada.Shared
```

**Estrutura**:
```
Shared/
├── Contracts/
│   ├── ApiResponse.cs
│   ├── PagedResponse.cs
│   └── ValidationError.cs
├── Dtos/
│   ├── Auth/
│   ├── Usuario/
│   └── Docs/
├── Enums/
│   └── StatusUsuario.cs
└── Extensions/
    └── StringExtensions.cs
```

#### 1.2 Criar Projeto Core
```bash
dotnet new classlib -n PortalSaudeConectada.Core -o PortalSaudeConectada/PortalSaudeConectada.Core
dotnet sln add PortalSaudeConectada/PortalSaudeConectada.Core
```

**Estrutura**:
```
Core/
├── Domain/
│   ├── Entities/
│   │   ├── BaseEntity.cs
│   │   ├── Usuario.cs
│   │   └── DocVersion.cs
│   ├── Services/
│   │   └── IUsuarioService.cs
│   └── ValueObjects/
│       ├── Email.cs
│       └── Cpf.cs
├── Infrastructure/
│   ├── Data/
│   │   ├── PortalDbContext.cs
│   │   └── Configurations/
│   ├── Repositories/
│   │   ├── IRepository.cs
│   │   ├── Repository.cs
│   │   └── IUsuarioRepository.cs
│   └── Services/
│       └── Auth/
│           ├── IJwtService.cs
│           └── JwtService.cs
└── DependencyInjection.cs
```

#### 1.3 Remover Projeto ApiService (Duplicado)
```bash
dotnet sln remove PortalSaudeConectada/PortalSaudeConectada.ApiService
# Mover código útil para Api antes de deletar
```

#### 1.4 Configurar Dependências

**Api.csproj**:
```xml
<ItemGroup>
  <ProjectReference Include="..\PortalSaudeConectada.Core\PortalSaudeConectada.Core.csproj" />
  <ProjectReference Include="..\PortalSaudeConectada.Shared\PortalSaudeConectada.Shared.csproj" />
  <ProjectReference Include="..\PortalSaudeConectada.ServiceDefaults\PortalSaudeConectada.ServiceDefaults.csproj" />
</ItemGroup>
```

**Web.csproj**:
```xml
<ItemGroup>
  <ProjectReference Include="..\PortalSaudeConectada.Shared\PortalSaudeConectada.Shared.csproj" />
  <ProjectReference Include="..\PortalSaudeConectada.ServiceDefaults\PortalSaudeConectada.ServiceDefaults.csproj" />
</ItemGroup>
```

---

### FASE 2: Implementação de Padrões (2-3 dias)

#### 2.1 Instalar Pacotes Necessários

**Core.csproj**:
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.0" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
```

**Api.csproj**:
```xml
<PackageReference Include="Carter" Version="8.2.1" />
<PackageReference Include="MediatR" Version="12.4.1" />
<PackageReference Include="FluentValidation" Version="11.11.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.11.0" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
```

#### 2.2 Implementar Base Classes

1. **BaseEntity.cs** (Core/Domain/Entities)
2. **Repository.cs** (Core/Infrastructure/Repositories)
3. **ApiResponse.cs** (Shared/Contracts)
4. **PagedResponse.cs** (Shared/Contracts)

#### 2.3 Migrar DbContext

Mover `PortalDbContext` de `Api/Data` para `Core/Infrastructure/Data`

---

### FASE 3: Refatoração de Features (3-4 dias)

#### 3.1 Reestruturar Feature Auth

**Antes**:
```
Features/
└── AuthEndpoints.cs
```

**Depois**:
```
Features/
└── Auth/
    ├── AuthEndpoints.cs (Carter Module)
    ├── Login/
    │   ├── LoginEndpoint.cs
    │   ├── LoginRequest.cs
    │   ├── LoginResponse.cs
    │   ├── LoginHandler.cs
    │   └── LoginValidator.cs
    └── RefreshToken/
        ├── RefreshTokenEndpoint.cs
        ├── RefreshTokenRequest.cs
        ├── RefreshTokenResponse.cs
        └── RefreshTokenHandler.cs
```

#### 3.2 Reestruturar Demais Features

- Usuario
- Docs
- Metrics
- Team

Seguir mesmo padrão da Feature Auth.

---

### FASE 4: Implementação de Segurança (2-3 dias)

#### 4.1 Configurar Keycloak no AppHost

```csharp
var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

var api = builder.AddProject<Projects.PortalSaudeConectada_Api>("api")
    .WithReference(keycloak);
```

#### 4.2 Implementar JWT Service

- `IJwtService.cs`
- `JwtService.cs`
- Configuração em `Program.cs`

#### 4.3 Implementar RBAC

- Policies
- Claims Transformation
- Authorization Handlers

---

### FASE 5: Frontend (2-3 dias)

#### 5.1 Reestruturar Components

```
Web/
└── Components/
    ├── Layout/
    │   ├── MainLayout.razor
    │   ├── NavMenu.razor
    │   └── HeaderBar.razor
    ├── Pages/
    │   ├── Auth/
    │   ├── Usuario/
    │   └── Dashboard/
    └── Shared/
        ├── ConfirmDialog.razor
        └── DataTable.razor
```

#### 5.2 Criar HTTP Clients

```csharp
Services/
└── HttpClients/
    ├── IAuthClient.cs
    ├── AuthClient.cs
    ├── IUsuarioClient.cs
    └── UsuarioClient.cs
```

---

### FASE 6: Documentação (1-2 dias)

#### 6.1 Criar ADRs

- ADR-001: Escolha de .NET Aspire
- ADR-002: Vertical Slice Architecture
- ADR-003: Keycloak para Autenticação

#### 6.2 Atualizar README

Seguir template `readme-projeto.md`

#### 6.3 Criar Documentação Técnica

- Technical Spec
- Deployment Guide
- Runbook Operacional

---

### FASE 7: Testes (2-3 dias)

#### 7.1 Testes Unitários

- Handlers
- Validators
- Services

#### 7.2 Testes de Integração

- API Endpoints
- Database

#### 7.3 Testes de Componentes

- bUnit para Blazor

---

## 📊 Cronograma Estimado

| Fase | Duração | Prioridade |
|------|---------|------------|
| FASE 1: Reestruturação | 1-2 dias | 🔴 Alta |
| FASE 2: Padrões | 2-3 dias | 🔴 Alta |
| FASE 3: Features | 3-4 dias | 🔴 Alta |
| FASE 4: Segurança | 2-3 dias | 🔴 Alta |
| FASE 5: Frontend | 2-3 dias | 🟡 Média |
| FASE 6: Documentação | 1-2 dias | 🟡 Média |
| FASE 7: Testes | 2-3 dias | 🟡 Média |
| **TOTAL** | **13-20 dias** | |

---

## ✅ Checklist de Adequação

### Arquitetura
- [ ] Projeto Shared criado
- [ ] Projeto Core criado
- [ ] Projeto ApiService removido
- [ ] Dependências configuradas
- [ ] Estrutura de pastas conforme BIBLIOTECA_MODELO

### Código
- [ ] MediatR instalado e configurado
- [ ] Carter instalado e configurado
- [ ] FluentValidation instalado e configurado
- [ ] AutoMapper instalado e configurado
- [ ] BaseEntity implementado
- [ ] Repository Pattern implementado
- [ ] Features reestruturadas (Vertical Slice)

### Segurança
- [ ] Keycloak configurado
- [ ] JWT Service implementado
- [ ] RBAC implementado
- [ ] Endpoints protegidos

### Frontend
- [ ] Components reestruturados
- [ ] HTTP Clients criados
- [ ] State Management implementado

### Documentação
- [ ] ADRs criados
- [ ] README atualizado
- [ ] Technical Spec criado
- [ ] Deployment Guide criado
- [ ] Runbook criado

### Testes
- [ ] Testes unitários implementados
- [ ] Testes de integração implementados
- [ ] Testes de componentes implementados

---

**Próximo Passo**: Iniciar FASE 1 - Reestruturação da Solução

