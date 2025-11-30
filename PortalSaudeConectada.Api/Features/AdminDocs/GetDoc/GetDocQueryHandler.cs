using Microsoft.EntityFrameworkCore;
using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Core.Infrastructure.Data;
using PortalSaudeConectada.Shared.Contracts;
using DocDetailResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocDetailResponse;
using DocVersionInfoDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocVersionInfo;

namespace PortalSaudeConectada.Api.Features.AdminDocs.GetDoc;

public sealed class GetDocQueryHandler : IQueryHandler<GetDocQuery, ApiResponse<DocDetailResponseDto>>
{
    private readonly PortalDbContext _db;
    private readonly ILogger<GetDocQueryHandler> _logger;

    public GetDocQueryHandler(PortalDbContext db, ILogger<GetDocQueryHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ApiResponse<DocDetailResponseDto>> Handle(GetDocQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo documento: {Projeto}/{Filepath}", request.Projeto, request.Filepath);

        // TODO: Buscar conteúdo real do arquivo
        var versions = await _db.DocVersions
            .Where(d => d.Project.ToLower() == request.Projeto.ToLower() && d.FilePath == request.Filepath)
            .OrderByDescending(d => d.CreatedAt)
            .Take(5)
            .ToListAsync(cancellationToken);

        if (!versions.Any())
        {
            _logger.LogWarning("Documento não encontrado: {Projeto}/{Filepath}", request.Projeto, request.Filepath);
            return ApiResponse<DocDetailResponseDto>.Fail($"Documento '{request.Filepath}' não encontrado no projeto '{request.Projeto}'");
        }

        var latest = versions.First();

        var response = new DocDetailResponseDto
        {
            Projeto = request.Projeto,
            Filepath = request.Filepath,
            Version = latest.Version,
            Content = latest.Content,
            Author = latest.Author ?? "Unknown",
            CreatedAt = latest.CreatedAt,
            CommitHash = latest.CommitHash,
            Versions = versions.Select(v => new DocVersionInfoDto
            {
                Version = v.Version,
                Author = v.Author ?? "Unknown",
                CreatedAt = v.CreatedAt,
                CommitHash = v.CommitHash
            }).ToList()
        };

        _logger.LogInformation("Documento obtido com sucesso: {Projeto}/{Filepath}", request.Projeto, request.Filepath);
        return ApiResponse<DocDetailResponseDto>.Ok(response);
    }
}

