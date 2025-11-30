# 🔧 Troubleshooting - Scalar UI

## Problema: `/scalar/v1` não abre nenhuma página

### Possíveis Causas e Soluções

#### 1. ✅ Verificar se a API está rodando

**Sintoma**: A URL `https://localhost:7122/scalar/v1` não carrega.

**Solução**:
1. Verifique se a API está realmente rodando
2. Tente acessar primeiro: `http://localhost:5087/api/public/status`
3. Se o status funcionar, a API está rodando

#### 2. ✅ Verificar o Ambiente

**Problema**: Scalar só está configurado para ambiente `Development`.

**Solução**:
1. Verifique se `ASPNETCORE_ENVIRONMENT=Development`
2. No terminal, execute:
   ```bash
   dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj --environment Development
   ```

#### 3. ✅ Verificar a Rota Correta

**Rotas disponíveis**:
- ✅ `/scalar/v1` - Interface Scalar UI
- ✅ `/openapi/v1.json` - Documento OpenAPI JSON

**Teste**:
1. Primeiro, teste se o OpenAPI está acessível:
   ```bash
   curl http://localhost:5087/openapi/v1.json
   ```
2. Se funcionar, teste o Scalar:
   ```bash
   curl http://localhost:5087/scalar/v1
   ```

#### 4. ✅ Executar via AppHost (Recomendado)

**Problema**: Quando executado via AppHost, a API pode estar em uma porta diferente.

**Solução**:
1. Execute o AppHost:
   ```bash
   dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
   ```

2. Abra o dashboard do Aspire (geralmente `http://localhost:15000` ou `http://localhost:15001`)

3. No dashboard, procure o serviço **api** e veja qual porta foi atribuída

4. Clique no link do serviço **api** para ver os endpoints

5. Adicione `/scalar/v1` à URL da API

**Exemplo**:
- Se a API estiver em `http://localhost:5000`
- Acesse `http://localhost:5000/scalar/v1`

#### 5. ✅ Executar API Standalone (Desenvolvimento)

**Para testar sem o AppHost**:

```bash
# HTTP (porta 5087)
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj --launch-profile http
```

Depois acesse:
- **Scalar UI**: `http://localhost:5087/scalar/v1`
- **OpenAPI JSON**: `http://localhost:5087/openapi/v1.json`
- **Status**: `http://localhost:5087/api/public/status`

Ou HTTPS (porta 7122):
```bash
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj --launch-profile https
```

Depois acesse:
- **Scalar UI**: `https://localhost:7122/scalar/v1`
- **OpenAPI JSON**: `https://localhost:7122/openapi/v1.json`

#### 6. ✅ Verificar Logs da Aplicação

Quando a API iniciar, você deve ver logs como:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5087
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

Se não ver esses logs, a aplicação não iniciou corretamente.

#### 7. ✅ Verificar Connection String

**Problema**: A API pode não iniciar se a connection string estiver incorreta.

**Solução**:
1. Verifique se você está executando via AppHost (que injeta a connection string)
2. Ou verifique se `appsettings.Development.json` tem a connection string correta

Ver: `Docs/EXECUCAO-VIA-APPHOST.md`

#### 8. ✅ Testar com cURL

**Teste 1: Status da API**
```bash
curl http://localhost:5087/api/public/status
```

Resposta esperada:
```json
{
  "status": "healthy",
  "version": "1.0.0",
  ...
}
```

**Teste 2: OpenAPI JSON**
```bash
curl http://localhost:5087/openapi/v1.json
```

Resposta esperada: JSON com a especificação OpenAPI

**Teste 3: Scalar UI**
```bash
curl http://localhost:5087/scalar/v1
```

Resposta esperada: HTML da interface Scalar

#### 9. ✅ Verificar Firewall/Antivírus

Alguns firewalls ou antivírus podem bloquear portas locais.

**Solução**:
1. Temporariamente desabilite o firewall/antivírus
2. Ou adicione exceção para as portas 5087 e 7122

#### 10. ✅ Limpar e Recompilar

Se nada funcionar, tente limpar e recompilar:

```bash
# Limpar
dotnet clean PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj

# Recompilar
dotnet build PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj

# Executar
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj --launch-profile http
```

## 🎯 Checklist Rápido

- [ ] API está rodando?
- [ ] Ambiente é Development?
- [ ] Porta está correta? (5087 para HTTP, 7122 para HTTPS)
- [ ] Rota está correta? (`/scalar/v1`)
- [ ] OpenAPI JSON está acessível? (`/openapi/v1.json`)
- [ ] Connection string está configurada?
- [ ] Firewall não está bloqueando?

## 📝 Exemplo Completo de Execução

### Via AppHost (Recomendado)

```bash
# 1. Execute o AppHost
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj

# 2. Aguarde a mensagem de inicialização

# 3. Abra o dashboard do Aspire
# Geralmente: http://localhost:15000

# 4. No dashboard, clique no serviço "api"

# 5. Veja a URL da API (exemplo: http://localhost:5000)

# 6. Acesse: http://localhost:5000/scalar/v1
```

### Standalone (Desenvolvimento)

```bash
# 1. Execute a API
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj --launch-profile http

# 2. Aguarde a mensagem:
# "Now listening on: http://localhost:5087"

# 3. Acesse: http://localhost:5087/scalar/v1
```

## 🆘 Ainda não funciona?

Se após todas essas verificações ainda não funcionar:

1. **Verifique os logs da aplicação** - Pode haver erros de inicialização
2. **Teste o endpoint de status** - `http://localhost:5087/api/public/status`
3. **Verifique se o pacote Scalar está instalado** - `dotnet list package | findstr Scalar`
4. **Compartilhe os logs** - Copie a saída completa do console

## 📚 Recursos Adicionais

- [Scalar Documentation](https://github.com/scalar/scalar)
- [ASP.NET Core OpenAPI](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi)
- [EXECUCAO-VIA-APPHOST.md](./EXECUCAO-VIA-APPHOST.md)

