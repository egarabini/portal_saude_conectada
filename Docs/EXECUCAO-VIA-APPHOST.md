# Execução via AppHost (.NET Aspire)

## 🎯 Fluxo Correto de Execução

### ✅ Forma Correta: Via AppHost

O **AppHost** é o orquestrador do .NET Aspire que gerencia todos os projetos e suas configurações.

```bash
# Executar o AppHost
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
```

**O que acontece:**
1. AppHost lê a connection string do seu `appsettings.json`
2. AppHost inicia o Redis (cache)
3. AppHost inicia a ApiService
4. AppHost inicia a API e **injeta** a connection string via variável de ambiente `ConnectionStrings__DefaultConnection`
5. AppHost inicia o Web Frontend
6. Dashboard do Aspire fica disponível em `http://localhost:15000` (ou similar)

### ❌ Forma Incorreta: Executar API Diretamente

**NÃO** execute a API diretamente quando estiver usando Aspire:

```bash
# ❌ NÃO FAÇA ISSO quando usar Aspire
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj
```

**Por quê?**
- A API não receberá a connection string do AppHost
- A API não terá acesso ao Redis configurado
- A API não terá as configurações de ambiente corretas
- O dashboard do Aspire não estará disponível

## 🔍 Como Funciona a Injeção de Connection String

### 1. AppHost lê a configuração

<augment_code_snippet path="PortalSaudeConectada/PortalSaudeConectada.AppHost/appsettings.json" mode="EXCERPT">
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=161.97.141.186;Port=5432;...",
    "GCCuidadoDEV": "Host=161.97.141.186;Port=5432;..."
  },
  "Database": {
    "Target": "GCCuidadoDEV"
  }
}
```
</augment_code_snippet>

### 2. AppHost injeta via variável de ambiente

<augment_code_snippet path="PortalSaudeConectada/PortalSaudeConectada.AppHost/AppHost.cs" mode="EXCERPT">
```csharp
var api = builder.AddProject<Projects.PortalSaudeConectada_Api>("api")
    .WithEnvironment("ConnectionStrings__DefaultConnection", connectionString);
```
</augment_code_snippet>

**Nota**: A variável de ambiente usa `__` (dois underscores) para representar `:` na hierarquia de configuração.

### 3. API recebe a variável de ambiente

<augment_code_snippet path="PortalSaudeConectada/PortalSaudeConectada.Api/Program.cs" mode="EXCERPT">
```csharp
// O .NET Configuration automaticamente lê variáveis de ambiente
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Isso lê de ConnectionStrings__DefaultConnection (variável de ambiente)
// OU de appsettings.json (fallback)
```
</augment_code_snippet>

## 📊 Ordem de Precedência da Configuração

O .NET Configuration usa esta ordem (do menor para o maior):

1. `appsettings.json` (da API)
2. `appsettings.{Environment}.json` (da API)
3. **Variáveis de Ambiente** ← AppHost injeta aqui (SOBRESCREVE os anteriores)
4. Command-line arguments

Portanto, quando executado via AppHost:
- A variável de ambiente `ConnectionStrings__DefaultConnection` **sobrescreve** qualquer valor em `appsettings.json`

## 🚀 Executando o AppHost

### Via Visual Studio

1. Defina `PortalSaudeConectada.AppHost` como projeto de inicialização
2. Pressione F5 ou clique em "Run"
3. O dashboard do Aspire abrirá automaticamente

### Via Linha de Comando

```bash
cd PortalSaudeConectada
dotnet run --project PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
```

### Via Rider

1. Abra a configuração de execução
2. Selecione `PortalSaudeConectada.AppHost`
3. Clique em Run

## 🔍 Verificando se Está Funcionando

### 1. Logs do AppHost

Você deve ver:
```
=== AppHost - Configuração de Banco de Dados ===
Database:Target = GCCuidadoDEV
✓ Connection string encontrada: Host=161.97.141.186;Port=5432;Database=GCCui...
```

### 2. Logs da API

Você deve ver:
```
✓ Connection string encontrada (fonte: AppHost)
```

### 3. Dashboard do Aspire

Acesse `http://localhost:15000` (ou a porta mostrada no console) e verifique:
- ✅ Redis: Running
- ✅ apiservice: Running
- ✅ api: Running
- ✅ webfrontend: Running

## 🐛 Troubleshooting

### Erro: "Connection string 'DefaultConnection' não configurada no AppHost"

**Causa**: O `appsettings.json` do AppHost não tem a connection string.

**Solução**: Verifique `PortalSaudeConectada.AppHost/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Port=...;Database=...;Username=...;Password=..."
  }
}
```

### Erro: "Connection string 'DefaultConnection' não configurada" (na API)

**Causa**: A API não está recebendo a variável de ambiente do AppHost.

**Possíveis soluções**:
1. Certifique-se de estar executando via AppHost, não diretamente
2. Verifique se o AppHost está injetando a variável:
   ```csharp
   .WithEnvironment("ConnectionStrings__DefaultConnection", connectionString)
   ```
3. Verifique os logs do AppHost para confirmar que a connection string foi encontrada

### API inicia mas não conecta ao banco

**Causa**: Connection string incorreta ou banco indisponível.

**Solução**:
1. Verifique se o PostgreSQL está rodando em `161.97.141.186:5432`
2. Teste a connection string manualmente:
   ```bash
   psql -h 161.97.141.186 -p 5432 -U postgres -d GCCuidadoDEV
   ```
3. Verifique firewall e regras de rede

## 📝 Resumo

✅ **SEMPRE** execute via AppHost quando usar .NET Aspire  
✅ AppHost gerencia connection strings e configurações  
✅ AppHost injeta configurações via variáveis de ambiente  
✅ Use o dashboard do Aspire para monitorar os serviços  

❌ **NÃO** execute projetos individuais diretamente  
❌ **NÃO** configure connection strings em cada projeto  
❌ **NÃO** ignore os logs do AppHost  

