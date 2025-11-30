# Configuração da Connection String

## 📋 Problema Identificado

Ao compilar a API standalone (sem o AppHost), ela não reconhecia a `DefaultConnection` porque:
1. O `appsettings.json` da API não tinha a connection string configurada
2. A connection string só estava sendo passada via variável de ambiente pelo AppHost

## ✅ Solução Implementada

### 1. Adicionada Connection String no `appsettings.json` da API

**Arquivo**: `PortalSaudeConectada.Api/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB"
  }
}
```

### 2. Adicionada Connection String no `appsettings.Development.json`

**Arquivo**: `PortalSaudeConectada.Api/appsettings.Development.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB"
  }
}
```

## 🔄 Como Funciona Agora

### Cenário 1: Executando a API Standalone

```bash
cd PortalSaudeConectada/PortalSaudeConectada.Api
dotnet run
```

A API irá:
1. Ler a connection string do `appsettings.json` ou `appsettings.Development.json`
2. Conectar ao banco de dados `GCCuidadoDEV`
3. Usar o schema `saude_conectada`

### Cenário 2: Executando via AppHost (.NET Aspire)

```bash
cd PortalSaudeConectada/PortalSaudeConectada.AppHost
dotnet run
```

O AppHost irá:
1. Ler a connection string do seu próprio `appsettings.json`
2. Passar a connection string para a API via variável de ambiente `ConnectionStrings__DefaultConnection`
3. A variável de ambiente **sobrescreve** o valor do `appsettings.json` da API
4. A API usa a connection string passada pelo AppHost

## 🎯 Ordem de Precedência da Configuração

O .NET Configuration usa a seguinte ordem de precedência (do menor para o maior):

1. `appsettings.json`
2. `appsettings.{Environment}.json` (ex: `appsettings.Development.json`)
3. **Variáveis de Ambiente** ← AppHost injeta aqui
4. Command-line arguments

Isso significa que:
- **Standalone**: Usa `appsettings.json` ou `appsettings.Development.json`
- **Via AppHost**: Usa a variável de ambiente passada pelo AppHost (que sobrescreve os arquivos)

## 🔍 Como Verificar se Está Funcionando

### 1. Compilar a API

```bash
dotnet build PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj
```

**Resultado Esperado**: ✅ Build bem-sucedido sem erros

### 2. Executar a API Standalone

```bash
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj
```

**Resultado Esperado**: 
- API inicia sem erros
- Logs mostram conexão com o banco de dados
- Endpoint `/health` responde com status 200

### 3. Executar via AppHost

```bash
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
```

**Resultado Esperado**:
- AppHost inicia o dashboard do Aspire
- API aparece como "Running" no dashboard
- Logs mostram que a connection string foi injetada

## 🔐 Segurança

### ⚠️ IMPORTANTE: Não commitar senhas em produção!

Para ambientes de produção, use:

1. **Azure Key Vault** (recomendado para Azure)
2. **User Secrets** (para desenvolvimento local)
3. **Variáveis de Ambiente** (para containers/Docker)

### Configurar User Secrets (Desenvolvimento Local)

```bash
cd PortalSaudeConectada/PortalSaudeConectada.Api

# Inicializar user secrets
dotnet user-secrets init

# Adicionar connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB"
```

Depois, remover a connection string do `appsettings.json` e `appsettings.Development.json`.

## 📝 Resumo

✅ **Problema Resolvido**: API agora reconhece a `DefaultConnection`
✅ **Funciona Standalone**: Connection string no `appsettings.json`
✅ **Funciona via AppHost**: Connection string injetada via variável de ambiente
✅ **Build Bem-Sucedido**: Compilação sem erros

## 🔗 Arquivos Modificados

- `PortalSaudeConectada/PortalSaudeConectada.Api/appsettings.json`
- `PortalSaudeConectada/PortalSaudeConectada.Api/appsettings.Development.json`

## 🔗 Arquivos Relacionados

- `PortalSaudeConectada/PortalSaudeConectada.AppHost/AppHost.cs` - Configuração do AppHost
- `PortalSaudeConectada/PortalSaudeConectada.AppHost/appsettings.json` - Connection strings do AppHost
- `PortalSaudeConectada/PortalSaudeConectada.Api/Program.cs` - Leitura da connection string
- `PortalSaudeConectada/PortalSaudeConectada.Api/Data/PortalDbContext.cs` - DbContext com schema `saude_conectada`

