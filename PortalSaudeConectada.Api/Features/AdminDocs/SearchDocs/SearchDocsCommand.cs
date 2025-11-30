using PortalSaudeConectada.Core.Application.Commands;
using PortalSaudeConectada.Shared.Contracts;
using SearchDocsResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.SearchDocsResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.SearchDocs;

public sealed record SearchDocsCommand(string Query, string? Projeto = null, int Limit = 20) 
    : ICommand<ApiResponse<SearchDocsResponseDto>>;

