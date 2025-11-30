using Carter;
using PortalSaudeConectada.Core.Infrastructure.Data;
using PortalSaudeConectada.Shared.Contracts;

namespace PortalSaudeConectada.Api.Features.Admin.SeedDatabase;

/// <summary>
/// Endpoint para popular o banco de dados com dados iniciais
/// </summary>
public sealed class SeedDatabaseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/seed-database", HandleAsync)
            .WithName("SeedDatabase")
            .WithTags("🔧 Admin")
            .WithSummary("Popular banco de dados com dados iniciais")
            .WithDescription("Cria usuários de teste: admin, gestor e profissional")
            .Produces<ApiResponse<string>>(200)
            .Produces<ApiResponse<string>>(400)
            .Produces(401)
            .RequireAuthorization("AdminOnly");
    }

    private static async Task<IResult> HandleAsync(
        DbSeeder seeder,
        CancellationToken cancellationToken)
    {
        try
        {
            await seeder.SeedAsync();
            return Results.Ok(ApiResponse<string>.Ok(
                "Banco de dados populado com sucesso! Usuários criados: admin, gestor, profissional",
                "Seed executado com sucesso"));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ApiResponse<string>.Fail(
                $"Erro ao popular banco de dados: {ex.Message}"));
        }
    }
}

