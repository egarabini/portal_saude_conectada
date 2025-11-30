using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using BacklogResponseDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.BacklogResponse;
using BacklogItemDto = PortalSaudeConectada.Shared.Dtos.AdminTeam.BacklogItem;

namespace PortalSaudeConectada.Api.Features.AdminTeam.GetBacklog;

public sealed class GetBacklogQueryHandler : IQueryHandler<GetBacklogQuery, ApiResponse<BacklogResponseDto>>
{
    private readonly ILogger<GetBacklogQueryHandler> _logger;

    public GetBacklogQueryHandler(ILogger<GetBacklogQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<BacklogResponseDto>> Handle(GetBacklogQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo backlog");

        // TODO: Integrar com Azure DevOps, Jira ou GitHub Projects
        await Task.CompletedTask; // Simula operação assíncrona

        var response = new BacklogResponseDto
        {
            Total = 42,
            Items = new List<BacklogItemDto>
            {
                new() { Id = 1, Titulo = "Implementar autenticação Keycloak", Projeto = "portal", Prioridade = "alta", Status = "em-andamento" },
                new() { Id = 2, Titulo = "Criar dashboard de métricas", Projeto = "portal", Prioridade = "alta", Status = "planejado" },
                new() { Id = 3, Titulo = "Integração OTel no CarePlanner", Projeto = "careplanner", Prioridade = "media", Status = "planejado" }
            }
        };

        _logger.LogInformation("Backlog obtido. Total: {Total}", response.Total);
        return ApiResponse<BacklogResponseDto>.Ok(response);
    }
}

