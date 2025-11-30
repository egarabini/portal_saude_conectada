using Carter;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PortalSaudeConectada.Shared.Contracts;

namespace PortalSaudeConectada.Api.Features.Admin.InitializeDatabase;

/// <summary>
/// Endpoint para inicializar o schema do banco de dados
/// </summary>
public class InitializeDatabaseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/initialize-database", async (
            [FromServices] IConfiguration configuration) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                return Results.BadRequest(ApiResponse<object>.Fail("Connection string não configurada"));
            }

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = @"
-- Criar schema se não existir
CREATE SCHEMA IF NOT EXISTS saude_conectada;

-- Criar tabela usuarios
CREATE TABLE IF NOT EXISTS saude_conectada.usuarios (
    id uuid NOT NULL DEFAULT gen_random_uuid(),
    nome character varying(200) NOT NULL,
    email character varying(200) NOT NULL,
    username character varying(100) NOT NULL,
    password_hash character varying(500) NOT NULL,
    cpf character varying(14),
    telefone character varying(20),
    tipo integer NOT NULL,
    status integer NOT NULL,
    ultimo_login timestamp with time zone,
    email_verificado boolean NOT NULL DEFAULT false,
    email_verification_token character varying(500),
    password_reset_token character varying(500),
    password_reset_token_expires timestamp with time zone,
    created_at timestamp with time zone NOT NULL DEFAULT now(),
    created_by character varying(100),
    updated_at timestamp with time zone,
    updated_by character varying(100),
    is_deleted boolean NOT NULL DEFAULT false,
    deleted_at timestamp with time zone,
    deleted_by character varying(100),
    CONSTRAINT ""PK_usuarios"" PRIMARY KEY (id)
);

-- Criar índices para usuarios
CREATE UNIQUE INDEX IF NOT EXISTS ""IX_usuarios_email"" ON saude_conectada.usuarios (email);
CREATE UNIQUE INDEX IF NOT EXISTS ""IX_usuarios_username"" ON saude_conectada.usuarios (username);
CREATE INDEX IF NOT EXISTS ""IX_usuarios_cpf"" ON saude_conectada.usuarios (cpf);
CREATE INDEX IF NOT EXISTS ""IX_usuarios_status"" ON saude_conectada.usuarios (status);
CREATE INDEX IF NOT EXISTS ""IX_usuarios_is_deleted"" ON saude_conectada.usuarios (is_deleted);

-- Criar tabela refresh_tokens
CREATE TABLE IF NOT EXISTS saude_conectada.refresh_tokens (
    id uuid NOT NULL DEFAULT gen_random_uuid(),
    token character varying(500) NOT NULL,
    usuario_id uuid NOT NULL,
    expires_at timestamp with time zone NOT NULL,
    is_revoked boolean NOT NULL DEFAULT false,
    revoked_at timestamp with time zone,
    ip_address character varying(45),
    user_agent character varying(500),
    created_at timestamp with time zone NOT NULL DEFAULT now(),
    created_by character varying(100),
    updated_at timestamp with time zone,
    updated_by character varying(100),
    is_deleted boolean NOT NULL DEFAULT false,
    deleted_at timestamp with time zone,
    deleted_by character varying(100),
    CONSTRAINT ""PK_refresh_tokens"" PRIMARY KEY (id),
    CONSTRAINT ""FK_refresh_tokens_usuarios_usuario_id"" FOREIGN KEY (usuario_id) 
        REFERENCES saude_conectada.usuarios (id) ON DELETE CASCADE
);

-- Criar índices para refresh_tokens
CREATE UNIQUE INDEX IF NOT EXISTS ""ix_refresh_tokens_token"" ON saude_conectada.refresh_tokens (token);
CREATE INDEX IF NOT EXISTS ""ix_refresh_tokens_usuario_id"" ON saude_conectada.refresh_tokens (usuario_id);
CREATE INDEX IF NOT EXISTS ""ix_refresh_tokens_expires_at"" ON saude_conectada.refresh_tokens (expires_at);
";

                await using var command = new NpgsqlCommand(sql, connection);
                await command.ExecuteNonQueryAsync();

                return Results.Ok(ApiResponse<object>.Ok(
                    new
                    {
                        TablesCreated = new[] { "usuarios", "refresh_tokens" },
                        Schema = "saude_conectada"
                    },
                    "Schema do banco de dados inicializado com sucesso! Agora execute /api/admin/seed-database para popular os dados."
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<object>.Fail($"Erro ao inicializar banco: {ex.Message}"));
            }
        })
        .WithName("InitializeDatabase")
        .WithTags("Admin")
        .WithSummary("Inicializa o schema do banco de dados")
        .WithDescription("Cria as tabelas necessárias no schema saude_conectada. Execute este endpoint antes de seed-database.")
        .Produces<ApiResponse<object>>(200)
        .Produces<ApiResponse<object>>(400);
    }
}

