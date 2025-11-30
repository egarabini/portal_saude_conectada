# Documentação OpenAPI com Scalar

## 🎯 Visão Geral

A API do Portal Saúde Conectada usa **OpenAPI 3.0** para documentação automática e **Scalar** como interface de visualização (substituindo o Swagger UI tradicional).

## 🚀 Acessando a Documentação

### Via AppHost (Recomendado)

1. Execute o AppHost:
   ```bash
   dotnet run --project PortalSaudeConectada/PortalSaudeConectada.AppHost/PortalSaudeConectada.AppHost.csproj
   ```

2. Acesse o dashboard do Aspire (geralmente `http://localhost:15000`)

3. Clique no serviço **api** para ver os endpoints

4. Acesse a documentação Scalar:
   - **Scalar UI**: `https://localhost:7122/scalar/v1` ou `http://localhost:5087/scalar/v1`
   - **OpenAPI JSON**: `https://localhost:7122/openapi/v1.json` ou `http://localhost:5087/openapi/v1.json`

### Standalone (Desenvolvimento)

Se executar a API diretamente (não recomendado com Aspire):

```bash
dotnet run --project PortalSaudeConectada/PortalSaudeConectada.Api/PortalSaudeConectada.Api.csproj
```

Acesse:
- **Scalar UI**: `https://localhost:7122/scalar/v1`
- **OpenAPI JSON**: `https://localhost:7122/openapi/v1.json`

## 📚 Recursos da Documentação Scalar

### 1. Interface Moderna e Intuitiva

Scalar oferece uma interface muito mais moderna que o Swagger UI:
- ✅ Design limpo e responsivo
- ✅ Tema roxo personalizado
- ✅ Navegação por categorias (tags)
- ✅ Busca rápida de endpoints
- ✅ Exemplos de código em múltiplas linguagens

### 2. Categorias de Endpoints

A API está organizada em 5 categorias principais:

#### 🌐 Público
Endpoints acessíveis sem autenticação:
- `GET /api/public/status` - Status do sistema
- `GET /api/public/catalogos` - Catálogo de projetos
- `POST /api/public/contato` - Enviar mensagem de contato

#### 📊 Admin - Métricas
Endpoints administrativos para métricas:
- `GET /api/admin/metrics/overview` - Visão geral das métricas
- `GET /api/admin/metrics/{projeto}` - Métricas de um projeto

#### 📚 Admin - Documentação
Endpoints administrativos para documentação:
- `GET /api/admin/docs` - Listar documentos
- `GET /api/admin/docs/{projeto}/{filepath}` - Obter documento
- `POST /api/admin/docs/search` - Buscar documentos

#### 👥 Admin - Equipe
Endpoints administrativos para gestão de equipe:
- `GET /api/admin/team/backlog` - Obter backlog
- `GET /api/admin/team/sprints` - Obter sprints

#### 🔐 Autenticação
Endpoints de autenticação:
- `POST /api/auth/login` - Fazer login
- `POST /api/auth/refresh` - Renovar token
- `POST /api/auth/logout` - Fazer logout

### 3. Testando Endpoints

Scalar permite testar endpoints diretamente na interface:

1. Selecione um endpoint
2. Preencha os parâmetros necessários
3. Clique em "Send Request"
4. Veja a resposta em tempo real

### 4. Exemplos de Código

Scalar gera automaticamente exemplos de código em várias linguagens:
- C# (HttpClient)
- JavaScript (fetch)
- Python (requests)
- cURL
- E mais...

## 🔧 Configuração

### Metadados da API

Os metadados estão configurados em `Program.cs`:

```csharp
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Portal Saúde Conectada API",
            Version = "v1",
            Description = "...",
            Contact = new() { ... },
            License = new() { ... }
        };
        // ...
    });
});
```

### Configuração do Scalar

```csharp
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Portal Saúde Conectada API")
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithPreferredScheme("Bearer")
        .WithApiKeyAuthentication(x => x.Token = "your-api-key-here");
});
```

### Metadados dos Endpoints

Cada endpoint tem metadados detalhados:

```csharp
group.MapGet("/status", GetStatus)
    .WithName("GetStatus")
    .WithSummary("Status do sistema")
    .WithDescription("Retorna o status de saúde do sistema e dos serviços integrados")
    .Produces<StatusResponse>(200)
    .WithOpenApi();
```

## 📖 Documentação Completa

### Informações Incluídas

Para cada endpoint, a documentação inclui:
- ✅ **Nome e Descrição**: O que o endpoint faz
- ✅ **Método HTTP**: GET, POST, PUT, DELETE, etc.
- ✅ **Rota**: Caminho completo do endpoint
- ✅ **Parâmetros**: Query, path, body
- ✅ **Tipos de Resposta**: DTOs tipados
- ✅ **Códigos de Status**: 200, 400, 401, 404, etc.
- ✅ **Exemplos**: Request e response

### DTOs Tipados

Todos os endpoints usam DTOs (Data Transfer Objects) tipados:

```csharp
public record StatusResponse(
    string Status,
    string Version,
    DateTime Timestamp,
    ServicesStatus Services
);
```

Isso garante que a documentação OpenAPI seja precisa e completa.

## 🎨 Personalização

### Temas Disponíveis

Scalar suporta vários temas:
- `ScalarTheme.Purple` (atual)
- `ScalarTheme.Blue`
- `ScalarTheme.Green`
- `ScalarTheme.Dark`
- `ScalarTheme.Light`

Para mudar o tema, edite `Program.cs`:

```csharp
.WithTheme(ScalarTheme.Blue)
```

## 🔒 Autenticação na Documentação

### Testando Endpoints Autenticados

1. Faça login via `POST /api/auth/login`
2. Copie o `accessToken` da resposta
3. No Scalar, clique em "Authorize" (canto superior direito)
4. Cole o token no campo "Bearer Token"
5. Agora você pode testar endpoints protegidos

## 📥 Exportando a Documentação

### Baixar OpenAPI JSON

Acesse `https://localhost:7122/openapi/v1.json` e salve o arquivo.

### Importar em Outras Ferramentas

O arquivo OpenAPI pode ser importado em:
- **Postman**: Import → OpenAPI 3.0
- **Insomnia**: Import → OpenAPI
- **Swagger Editor**: File → Import URL
- **Redoc**: Gerar documentação estática

## 🚀 Próximos Passos

1. ✅ Documentação OpenAPI configurada
2. ✅ Scalar UI integrado
3. ✅ Todos os endpoints documentados
4. 🔲 Adicionar exemplos de request/response
5. 🔲 Adicionar schemas de validação
6. 🔲 Configurar autenticação JWT real
7. 🔲 Adicionar rate limiting

## 📚 Recursos Adicionais

- [Scalar Documentation](https://github.com/scalar/scalar)
- [OpenAPI Specification](https://spec.openapis.org/oas/latest.html)
- [ASP.NET Core OpenAPI](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi)

