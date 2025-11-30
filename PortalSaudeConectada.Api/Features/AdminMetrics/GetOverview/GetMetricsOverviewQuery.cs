using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using MetricsOverviewResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.MetricsOverviewResponse;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetOverview;

/// <summary>
/// Query para obter visão geral das métricas
/// </summary>
public sealed record GetMetricsOverviewQuery : IQuery<ApiResponse<MetricsOverviewResponseDto>>;

