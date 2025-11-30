using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using SearchDocsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.SearchDocsResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.SearchDocs;

public sealed class SearchDocsCommandHandler : ICommandHandler<SearchDocsCommand, ApiResponse<SearchDocsResponseDto>>
{
    private readonly ILogger<SearchDocsCommandHandler> _logger;

    public SearchDocsCommandHandler(ILogger<SearchDocsCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<SearchDocsResponseDto>> Handle(SearchDocsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando documentos: {Query}, Projeto: {Projeto}", request.Query, request.Projeto ?? "todos");

        // TODO: Implementar busca full-text (Postgres full-text search ou Elasticsearch)
        await Task.CompletedTask; // Simula operação assíncrona

        var response = new SearchDocsResponseDto
        {
            Query = request.Query,
            Total = 0,
            Results = new List<object>()
        };

        _logger.LogInformation("Busca concluída. Resultados: {Total}", response.Total);
        return ApiResponse<SearchDocsResponseDto>.Ok(response);
    }
}

