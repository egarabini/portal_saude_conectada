using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PortalSaudeConectada.Api.Features;
using PortalSaudeConectada.Core;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Aspire ServiceDefaults: OpenTelemetry, HealthChecks, Resiliência
builder.AddServiceDefaults();

// OpenAPI com metadados detalhados
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Portal Saúde Conectada API",
            Version = "v1",
            Description = """
                API Backend do Portal Saúde Conectada - Sistema de gestão e documentação para equipes de saúde.

                ## Funcionalidades

                ### 📊 Métricas (Admin)
                - Visualização de métricas de uso do portal
                - Estatísticas de acesso e engajamento
                - Cache de métricas para performance

                ### 📚 Documentação (Admin)
                - Gestão de documentos técnicos
                - Versionamento de documentação
                - Sincronização com repositório Git

                ### 👥 Equipe (Admin)
                - Gestão de membros da equipe
                - Perfis e permissões
                - Histórico de contribuições

                ### 🔐 Autenticação
                - Login via Keycloak
                - Tokens JWT
                - Refresh tokens

                ### 🌐 Público
                - Acesso a documentação pública
                - Consulta de métricas públicas
                - Informações da equipe
                """,
            Contact = new()
            {
                Name = "Equipe Portal Saúde Conectada",
                Email = "contato@saudeconectada.gov.br"
            },
            License = new()
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        document.Servers =
        [
            new() { Url = "https://localhost:7122", Description = "Development (HTTPS)" },
            new() { Url = "http://localhost:5087", Description = "Development (HTTP)" }
        ];

        return Task.CompletedTask;
    });
});

// CORS configurado
builder.Services.AddCors(options =>
{
    options.AddPolicy("PublicPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7001", "https://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Adiciona os serviços do Core (DbContext, Repositories, etc.)
builder.Services.AddCoreServices(builder.Configuration);

// MediatR - CQRS Pattern
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// FluentValidation - Request Validation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Carter - Minimal API Endpoint Organization
builder.Services.AddCarter();

// Autenticação JWT
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT Secret não configurado");
var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer não configurado");
var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("JWT Audience não configurado");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.Zero // Remove o delay padrão de 5 minutos
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

// Autorização com Policies baseadas em Roles
builder.Services.AddAuthorization(options =>
{
    // Policy para Administradores
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("tipo", "Administrador"));

    // Policy para Gestores e Administradores
    options.AddPolicy("GestorOrAdmin", policy =>
        policy.RequireClaim("tipo", "Administrador", "Gestor"));

    // Policy para qualquer usuário autenticado
    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

// TODO: Rate limiting para rotas públicas
// builder.Services.AddRateLimiter(...);

var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    // Mapeia o documento OpenAPI em /openapi/v1.json
    app.MapOpenApi();

    // Scalar UI - Interface moderna para documentação da API
    // Acessível em /scalar/v1
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Portal Saúde Conectada API")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

// Redireciona a raiz para a documentação Scalar
app.MapGet("/", () => Results.Redirect("/scalar/v1"))
    .ExcludeFromDescription();

app.UseHttpsRedirection();
app.UseCors();

// Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// TODO: app.UseRateLimiter();

// Endpoints de saúde padrão do Aspire
app.MapDefaultEndpoints();

// Carter - Mapeia todos os módulos automaticamente
app.MapCarter();

app.Run();
