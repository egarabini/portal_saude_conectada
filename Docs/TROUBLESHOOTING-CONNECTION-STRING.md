# Troubleshooting - Connection String

## ❌ Erro: "Connection string 'DefaultConnection' não configurada"

### Possíveis Causas e Soluções

#### 1. Arquivos appsettings.json não estão sendo lidos

**Verificar**:
```bash
# Verificar se os arquivos existem no diretório de output
ls PortalSaudeConectada/PortalSaudeConectada.Api/bin/Debug/net10.0/appsettings*.json
```

**Solução**: Os arquivos devem estar presentes. Se não estiverem, adicione ao `.csproj`:
```xml
<ItemGroup>
  <Content Include="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
  <Content Include="appsettings.Development.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

#### 2. Ambiente não está configurado corretamente

**Verificar**:
```bash
# No PowerShell
$env:ASPNETCORE_ENVIRONMENT
```

**Solução**: Definir a variável de ambiente:
```powershell
# PowerShell
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Ou no launchSettings.json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development"
}
```

#### 3. Connection string com formato incorreto

**Verificar** o conteúdo de `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB"
  }
}
```

**Atenção**: 
- A seção deve ser `"ConnectionStrings"` (plural)
- A chave deve ser `"DefaultConnection"` (exatamente como está)
- O valor deve ser uma string válida de conexão PostgreSQL

#### 4. Executando via AppHost mas a variável de ambiente não está sendo injetada

**Verificar** `PortalSaudeConectada.AppHost/AppHost.cs`:
```csharp
var api = builder.AddProject<Projects.PortalSaudeConectada_Api>("api")
    .WithEnvironment("ConnectionStrings__DefaultConnection", connectionString);
```

**Nota**: A variável de ambiente usa `__` (dois underscores) para separar níveis de configuração.

#### 5. ServiceDefaults está interferindo

**Verificar** se `AddServiceDefaults()` está sendo chamado **antes** de ler a connection string:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Aspire ServiceDefaults: OpenTelemetry, HealthChecks, Resiliência
builder.AddServiceDefaults(); // ← Deve vir ANTES

// Ler connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```

### Diagnóstico Passo a Passo

#### Passo 1: Verificar arquivos de configuração

```bash
# Verificar se os arquivos existem
cat PortalSaudeConectada/PortalSaudeConectada.Api/appsettings.json
cat PortalSaudeConectada/PortalSaudeConectada.Api/appsettings.Development.json
```

#### Passo 2: Verificar arquivos no diretório de output

```bash
# Após build
cat PortalSaudeConectada/PortalSaudeConectada.Api/bin/Debug/net10.0/appsettings.json
```

#### Passo 3: Adicionar logs de debug

Adicione no `Program.cs` **antes** de ler a connection string:
```csharp
// Debug: Listar todas as configurações
Console.WriteLine("=== Configurações Disponíveis ===");
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"ContentRootPath: {builder.Environment.ContentRootPath}");

var allConnectionStrings = builder.Configuration.GetSection("ConnectionStrings").GetChildren();
Console.WriteLine("=== Connection Strings ===");
foreach (var cs in allConnectionStrings)
{
    Console.WriteLine($"  {cs.Key}: {cs.Value?.Substring(0, Math.Min(30, cs.Value?.Length ?? 0))}...");
}
```

#### Passo 4: Testar leitura direta

```csharp
// Tentar diferentes formas de ler
var cs1 = builder.Configuration.GetConnectionString("DefaultConnection");
var cs2 = builder.Configuration["ConnectionStrings:DefaultConnection"];
var cs3 = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

Console.WriteLine($"GetConnectionString: {cs1 ?? "NULL"}");
Console.WriteLine($"Indexer: {cs2 ?? "NULL"}");
Console.WriteLine($"GetSection: {cs3 ?? "NULL"}");
```

### Solução Implementada no Código

O `Program.cs` atual usa uma abordagem com fallback:

```csharp
// Tenta obter a connection string de várias fontes (ordem de precedência):
// 1. Variável de ambiente ConnectionStrings__DefaultConnection (injetada pelo AppHost)
// 2. appsettings.Development.json (se ASPNETCORE_ENVIRONMENT=Development)
// 3. appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    // Fallback: tentar ler diretamente da seção ConnectionStrings
    connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    var errorMessage = $"Connection string 'DefaultConnection' não configurada. " +
                      $"Environment: {builder.Environment.EnvironmentName}, " +
                      $"ApplicationName: {builder.Environment.ApplicationName}";
    throw new InvalidOperationException(errorMessage);
}
```

### Teste Rápido

Execute este comando para testar se a API inicia:

```bash
cd PortalSaudeConectada/PortalSaudeConectada.Api
dotnet run
```

Se funcionar, você verá:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7122
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5087
```

Se não funcionar, você verá a mensagem de erro com detalhes do ambiente.

### Última Opção: Hardcode Temporário

Se nada funcionar, como teste temporário, adicione diretamente no código:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=161.97.141.186;Port=5432;Database=GCCuidadoDEV;Username=postgres;Password=Crazy#57LB";
```

⚠️ **ATENÇÃO**: Isso é apenas para teste! Não commite código com senha hardcoded!

### Contato para Suporte

Se o problema persistir, forneça:
1. Mensagem de erro completa
2. Valor de `ASPNETCORE_ENVIRONMENT`
3. Conteúdo de `appsettings.json` e `appsettings.Development.json`
4. Como está executando (Visual Studio, `dotnet run`, AppHost, etc.)

