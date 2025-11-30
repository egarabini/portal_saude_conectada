using Microsoft.EntityFrameworkCore;
using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Core.Infrastructure.Data;
using PortalSaudeConectada.Shared.Contracts;
using MetricsOverviewResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.MetricsOverviewResponse;
using ProjectMetricsSummaryDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.ProjectMetricsSummary;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetOverview;

/// <summary>
/// Handler para obter visão geral das métricas
/// </summary>
public sealed class GetMetricsOverviewQueryHandler : IQueryHandler<GetMetricsOverviewQuery, ApiResponse<MetricsOverviewResponseDto>>
{
    private readonly PortalDbContext _db;
    private readonly ILogger<GetMetricsOverviewQueryHandler> _logger;

    public GetMetricsOverviewQueryHandler(
        PortalDbContext db,
        ILogger<GetMetricsOverviewQueryHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ApiResponse<MetricsOverviewResponseDto>> Handle(
        GetMetricsOverviewQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo visão geral das métricas");

        // TODO: Buscar métricas reais via OTel ou APIs dos projetos
        var cachedMetrics = await _db.CachedMetrics
            .Where(m => m.ExpiresAt == null || m.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(m => m.CreatedAt)
            .Take(100)
            .ToListAsync(cancellationToken);

        var response = new MetricsOverviewResponseDto
        {
            Timestamp = DateTime.UtcNow,
            Projetos = new List<ProjectMetricsSummaryDto>
            {
                new()
                {
                    Id = "careplanner",
                    Nome = "CarePlanner",
                    Status = "operational",
                    Uptime = "99.8%",
                    ChecklistProgress = 42,
                    ChecklistTotal = 24,
                    Alerts = 0
                },
                new()
                {
                    Id = "conectarede",
                    Nome = "ConectaRede",
                    Status = "operational",
                    Uptime = "99.5%",
                    ChecklistProgress = 38,
                    ChecklistTotal = 45,
                    Alerts = 1
                },
                new()
                {
                    Id = "intellicaremcp",
                    Nome = "IntelliCare MCP",
                    Status = "development",
                    Uptime = "N/A",
                    ChecklistProgress = 15,
                    ChecklistTotal = 58,
                    Alerts = 0
                }
            },
            Cached = cachedMetrics.Count
        };

        _logger.LogInformation("Visão geral das métricas obtida com sucesso. {Count} projetos", response.Projetos.Count);
        return ApiResponse<MetricsOverviewResponseDto>.Ok(response);
    }
}

