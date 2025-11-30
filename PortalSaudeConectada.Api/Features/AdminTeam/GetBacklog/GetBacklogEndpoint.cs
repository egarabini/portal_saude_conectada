using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using BacklogResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.BacklogResponse;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetBacklog;

public sealed class GetBacklogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/team/backlog", HandleAsync)
            .WithName("GetBacklog")
            .WithTags("👥 Admin - Equipe")
            .WithSummary("Obter backlog")
            .WithDescription("Retorna os itens do backlog de todos os projetos")
            .Produces<ApiResponse<BacklogResponseDto>>(200)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetBacklogQuery();
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

