using Carter;
using MediatR;
using PortalSaudeConectada.Shared.Contracts;
using MetricsOverviewResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.MetricsOverviewResponse;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetOverview;

/// <summary>
/// Endpoint para obter visão geral das métricas
/// </summary>
public sealed class GetMetricsOverviewEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/metrics/overview", HandleAsync)
            .WithName("GetMetricsOverview")
            .WithTags("📊 Admin - Métricas")
            .WithSummary("Visão geral das métricas")
            .WithDescription("Retorna métricas consolidadas de todos os projetos do portal")
            .Produces<ApiResponse<MetricsOverviewResponseDto>>(200)
            .Produces(401)
            .RequireAuthorization("GestorOrAdmin");
    }

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetMetricsOverviewQuery();
        var response = await mediator.Send(query, cancellationToken);

        return response.Success 
            ? Results.Ok(response) 
            : Results.BadRequest(response);
    }
}

