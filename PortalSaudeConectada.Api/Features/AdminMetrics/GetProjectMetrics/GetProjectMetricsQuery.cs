using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using ProjectMetricsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminMetrics.ProjectMetricsResponse;

namespace PortalSaudeConectada.Api.Features.AdminMetrics.GetProjectMetrics;

/// <summary>
/// Query para obter métricas de um projeto específico
/// </summary>
public sealed record GetProjectMetricsQuery(string Projeto) : IQuery<ApiResponse<ProjectMetricsResponseDto>>;

