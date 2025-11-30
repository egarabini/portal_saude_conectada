using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using SprintsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.SprintsResponse;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetSprints;

public sealed class GetSprintsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/team/sprints", HandleAsync)
            .WithName("GetSprints")
            .WithTags("👥 Admin - Equipe")
            .WithSummary("Obter sprints")
            .WithDescription("Retorna informações sobre sprints atuais e passadas")
            .Produces<ApiResponse<SprintsResponseDto>>(200)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetSprintsQuery();
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

