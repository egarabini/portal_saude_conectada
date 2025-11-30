using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using CatalogosResponseDto = PortalSaudeConectada.Shared.Dtos.Public.CatalogosResponse;
using ProjetoInfoDto = PortalSaudeConectada.Shared.Dtos.Public.ProjetoInfo;
using FeatureInfoDto = PortalSaudeConectada.Shared.Dtos.Public.FeatureInfo;

namespace PortalSaudeConectada.Api.Features.Public.GetCatalogos;

public sealed class GetCatalogosQueryHandler : IQueryHandler<GetCatalogosQuery, ApiResponse<CatalogosResponseDto>>
{
    private readonly ILogger<GetCatalogosQueryHandler> _logger;

    public GetCatalogosQueryHandler(ILogger<GetCatalogosQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<CatalogosResponseDto>> Handle(GetCatalogosQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo catálogos");

        await Task.CompletedTask; // Simula operação assíncrona

        var response = new CatalogosResponseDto
        {
            Projetos = new List<ProjetoInfoDto>
            {
                new() { Id = "careplanner", Nome = "CarePlanner", Descricao = "Plataforma de Coordenação do Cuidado" },
                new() { Id = "conectarede", Nome = "ConectaRede", Descricao = "Integração RSC FHIR" },
                new() { Id = "intellicaremcp", Nome = "IntelliCare MCP", Descricao = "Automação e IA" }
            },
            Features = new List<FeatureInfoDto>
            {
                new() { Id = "dashboard", Nome = "Dashboard Executivo", Status = "em-desenvolvimento" },
                new() { Id = "docs", Nome = "Documentação Navegável", Status = "planejado" },
                new() { Id = "metricas", Nome = "Métricas Técnicas", Status = "planejado" }
            }
        };

        _logger.LogInformation("Catálogos obtidos. Projetos: {Projetos}, Features: {Features}", 
            response.Projetos.Count, response.Features.Count);
        return ApiResponse<CatalogosResponseDto>.Ok(response);
    }
}

