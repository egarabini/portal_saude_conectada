using Microsoft.EntityFrameworkCore;
using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Core.Infrastructure.Data;
using PortalSaudeConectada.Shared.Contracts;
using ProjectMetricsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.ProjectMetricsResponse;
using MetricDetailDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.MetricDetail;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetProjectMetrics;

/// <summary>
/// Handler para obter métricas de um projeto específico
/// </summary>
public sealed class GetProjectMetricsQueryHandler : IQueryHandler<GetProjectMetricsQuery, ApiResponse<ProjectMetricsResponseDto>>
{
    private readonly PortalDbContext _db;
    private readonly ILogger<GetProjectMetricsQueryHandler> _logger;

    public GetProjectMetricsQueryHandler(
        PortalDbContext db,
        ILogger<GetProjectMetricsQueryHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ApiResponse<ProjectMetricsResponseDto>> Handle(
        GetProjectMetricsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo métricas do projeto: {Projeto}", request.Projeto);

        var metrics = await _db.CachedMetrics
            .Where(m => m.Project.ToLower() == request.Projeto.ToLower())
            .Where(m => m.ExpiresAt == null || m.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(m => m.CreatedAt)
            .Take(50)
            .ToListAsync(cancellationToken);

        if (!metrics.Any())
        {
            _logger.LogWarning("Projeto '{Projeto}' não encontrado ou sem métricas", request.Projeto);
            return ApiResponse<ProjectMetricsResponseDto>.Fail($"Projeto '{request.Projeto}' não encontrado ou sem métricas");
        }

        var response = new ProjectMetricsResponseDto
        {
            Projeto = request.Projeto,
            Timestamp = DateTime.UtcNow,
            Metrics = metrics.Select(m => new MetricDetailDto
            {
                Name = m.MetricName,
                Value = m.MetricValue,
                Timestamp = m.CreatedAt
            }).ToList()
        };

        _logger.LogInformation("Métricas do projeto '{Projeto}' obtidas com sucesso. {Count} métricas", 
            request.Projeto, response.Metrics.Count);
        
        return ApiResponse<ProjectMetricsResponseDto>.Ok(response);
    }
}

