using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using DocsListResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocsListResponse;
using DocInfoDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocInfo;

namespace PortalSaudeConectada.Api.Features.AdminDocs.ListDocs;

public sealed class ListDocsQueryHandler : IQueryHandler<ListDocsQuery, ApiResponse<DocsListResponseDto>>
{
    private readonly ILogger<ListDocsQueryHandler> _logger;

    public ListDocsQueryHandler(ILogger<ListDocsQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<DocsListResponseDto>> Handle(ListDocsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listando documentos. Projeto: {Projeto}", request.Projeto ?? "todos");

        // TODO: Integrar com sistema de arquivos ou versionamento Git
        var allDocs = new List<DocInfoDto>
        {
            new() { Projeto = "careplanner", Path = "00-Resumo-Estruturado.md", Titulo = "Resumo Estruturado", Tipo = "overview" },
            new() { Projeto = "careplanner", Path = "99-Checklist-Aderencia.md", Titulo = "Checklist de Aderência", Tipo = "checklist" },
            new() { Projeto = "conectarede", Path = "00-Resumo-Estruturado.md", Titulo = "Resumo Estruturado", Tipo = "overview" },
            new() { Projeto = "conectarede", Path = "99-Checklist-Aderencia.md", Titulo = "Checklist de Aderência", Tipo = "checklist" },
            new() { Projeto = "intellicaremcp", Path = "00-Resumo-Estruturado.md", Titulo = "Resumo Estruturado", Tipo = "overview" },
            new() { Projeto = "intellicaremcp", Path = "99-Checklist-Aderencia.md", Titulo = "Checklist de Aderência", Tipo = "checklist" }
        };

        var filtered = string.IsNullOrEmpty(request.Projeto)
            ? allDocs
            : allDocs.Where(d => d.Projeto.Equals(request.Projeto, StringComparison.OrdinalIgnoreCase)).ToList();

        await Task.CompletedTask; // Simula operação assíncrona

        var response = new DocsListResponseDto
        {
            Total = filtered.Count,
            Docs = filtered
        };

        _logger.LogInformation("Documentos listados. Total: {Total}", response.Total);
        return ApiResponse<DocsListResponseDto>.Ok(response);
    }
}

