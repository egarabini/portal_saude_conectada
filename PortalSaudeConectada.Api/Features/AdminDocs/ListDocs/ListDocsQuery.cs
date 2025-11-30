using PortalSaudeConectada.Core.Application.Queries;
using PortalSaudeConectada.Shared.Contracts;
using DocsListResponseDto = PortalSaudeConectada.Shared.Dtos.AdminDocs.DocsListResponse;

namespace PortalSaudeConectada.Api.Features.AdminDocs.ListDocs;

public sealed record ListDocsQuery(string? Projeto = null) : IQuery<ApiResponse<DocsListResponseDto>>;

