using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using SprintsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.SprintsResponse;
using SprintDetailDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.SprintDetail;
using SprintSummaryDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.SprintSummary;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetSprints;

public sealed class GetSprintsQueryHandler : IQueryHandler<GetSprintsQuery, ApiResponse<SprintsResponseDto>>
{
    private readonly ILogger<GetSprintsQueryHandler> _logger;

    public GetSprintsQueryHandler(ILogger<GetSprintsQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<SprintsResponseDto>> Handle(GetSprintsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo sprints");

        // TODO: Integrar com Azure DevOps, Jira ou GitHub Projects
        await Task.CompletedTask; // Simula operação assíncrona

        var response = new SprintsResponseDto
        {
            SprintAtual = new SprintDetailDto
            {
                Id = "sprint-24",
                Nome = "Sprint 24 - Nov/2025",
                Inicio = new DateTime(2025, 11, 18),
                Fim = new DateTime(2025, 12, 1),
                Progresso = 65,
                ItensCompletos = 13,
                ItensTotal = 20
            },
            Sprints = new List<SprintSummaryDto>
            {
                new() { Id = "sprint-23", Nome = "Sprint 23 - Nov/2025", Status = "concluido", Progresso = 100 },
                new() { Id = "sprint-24", Nome = "Sprint 24 - Nov/2025", Status = "em-andamento", Progresso = 65 },
                new() { Id = "sprint-25", Nome = "Sprint 25 - Dez/2025", Status = "planejado", Progresso = 0 }
            }
        };

        _logger.LogInformation("Sprints obtidas. Total: {Total}", response.Sprints.Count);
        return ApiResponse<SprintsResponseDto>.Ok(response);
    }
}

